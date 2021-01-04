using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };

            var customers = new List<Customer>
            {
                new Customer { Name = "Phil"},
                new Customer { Name = "Fatma"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers,
            };

            return View(viewModel);
        }


        [Route("movies")]
        public ViewResult Index()
        {
            var movies = _context.Movies.Include(c => c.Genre).ToList();
            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie != null)
                return View(movie);
            else
                return new HttpNotFoundResult();
        }


        public ActionResult Index(int? id, string sortedBy)
        {
            if (!id.HasValue)
                id = 1;

            if (String.IsNullOrWhiteSpace(sortedBy))
                sortedBy = "Name";

            //return Content($"The id={id} and sortedBy={sortedBy}");
            return Content(string.Format("The id={0} and sortedBy={1}", id, sortedBy));
        }

        [Route("movies/released/{year:regex(\\d{2})}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        public ActionResult Add()
        {
            return MovieFormView(title: "Add");
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return new HttpNotFoundResult();

            MovieFormViewModel mfvm = new MovieFormViewModel(movie);

            return MovieFormView(title: "Edit", movieFormViewModel: mfvm);

        }

        private ActionResult MovieFormView(string title, MovieFormViewModel movieFormViewModel = null)
        {
            if (movieFormViewModel == null) movieFormViewModel = new MovieFormViewModel();
           
            movieFormViewModel.Genres = GetGenresOrderedByAlpha();

            ViewBag.Title = title;
            return View("MovieForm", movieFormViewModel);
        }


        private IOrderedEnumerable<Genre> GetGenresOrderedByAlpha()
        {
            return _context.Genres.ToList().OrderBy(g => g.Name);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(MovieFormViewModel movievm)
        {
            if (!ModelState.IsValid)
            {
                return MovieFormView(movievm.Id == 0 ? "New" : "Edit", movievm);
            }

            Movie movie;
            if (movievm.Id == 0)
            {
                movie = new Movie();
                movie.AddedDate = DateTime.Now;
            }
            else
                movie = _context.Movies.SingleOrDefault(mov => mov.Id == movievm.Id);

            movie.Name = movievm.Name;
            movie.ReleaseDate = movievm.ReleaseDate.Value;
            movie.GenreId = movievm.GenreId.Value;
            movie.CurrentStock = movievm.CurrentStock.Value;

            if (movievm.Id == 0)
                _context.Movies.Add(movie);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
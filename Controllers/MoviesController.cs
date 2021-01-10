using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");

            return View("ListReadOnly");
        }

        public ActionResult Details(int id)
        {
            var movie = GetMovieFromDB(id);

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

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Add()
        {
            return MovieFormView(title: "Add");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = GetMovieFromDB(id);

            if (movie == null)
                return new HttpNotFoundResult();

            MovieFormViewModel mfvm = new MovieFormViewModel(movie);

            return MovieFormView(title: "Edit", movieFormViewModel: mfvm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(MovieFormViewModel movieFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                return MovieFormView(movieFormViewModel.Id == 0 ? "New" : "Edit", movieFormViewModel);
            }


            Movie movie;
            if (movieFormViewModel.Id == 0)
            {
                movie = new Movie(DateTime.Now);
                MovieFormViewModel.PopulateMovieFromViewModel(movieFormViewModel, movie);
                _context.Movies.Add(movie);
            }
            else
            {
                movie = GetMovieFromDB(movieFormViewModel.Id);
                MovieFormViewModel.PopulateMovieFromViewModel(movieFormViewModel, movie);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
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

        private Movie GetMovieFromDB(int id)
        {
            return _context.Movies.Include(m => m.Genre).SingleOrDefault(mov => mov.Id == id);
        }
    }
}
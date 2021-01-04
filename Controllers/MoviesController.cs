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

            return MovieFormView(title: "Edit", movie : movie);

        }

        private ActionResult MovieFormView(string title, Movie movie = null)
        {
            var viewModel = new MovieFormViewModel
            {
                Genres = GetGenresOrderedByAlpha(),
                Movie = movie
            };

            ViewBag.Title = title;
            return View("MovieForm", viewModel);
        }

        private IOrderedEnumerable<Genre> GetGenresOrderedByAlpha()
        {
            return _context.Genres.ToList().OrderBy(g => g.Name);
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                movie.AddedDate = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.SingleOrDefault(mov => mov.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.AddedDate = DateTime.Now;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.CurrentStock = movie.CurrentStock;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
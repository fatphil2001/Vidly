using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
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
            
            //return Content("Wawaaaa ");
            //return HttpNotFound("Gone! Is no longer there...");
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new { page = 1, toto = "happy" });
        }

        public ActionResult Edit(int id)
        {
            return Content("id=" + id);
        }

        public ActionResult Index(int? id, string sortedBy)
        {
            if (!id.HasValue)
                id = 1;

            if (String.IsNullOrWhiteSpace(sortedBy))
                sortedBy = "Name";

            //return Content($"The id={id} and sortedBy={sortedBy}");
            return Content( string.Format("The id={0} and sortedBy={1}", id, sortedBy));
        }

        [Route("movies/released/{year:regex(\\d{2})}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

    }
}
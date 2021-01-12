using System;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
//using System.Data.Entity;


namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // Customer rents movies
        // 
        [HttpPost]
        public IHttpActionResult RentMovies(NewRentalDto rentalDto)
        {
            // Private API so no need to do single or default 
            Customer customer = _context.Customers.Single(c => c.Id == rentalDto.CustomerId);

            //Get the movies from the DB with matching IDs
            var movies = _context.Movies.Where(m => rentalDto.MovieIds.Contains(m.Id));

            foreach (var movie in movies)
            {
                // create a new rental record.
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie out of stock");


                movie.NumberAvailable--;

                Rental rental = new Rental() { Movie = movie, Customer = customer, DateRented = DateTime.Now };
                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        }

    }
}

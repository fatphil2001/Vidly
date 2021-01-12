using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        // Customer rents movies
        // 
        [HttpPost]
        public IHttpActionResult RentMovies(NewRentalDto rentalDto)
        {
            return Ok();
        }

    }
}

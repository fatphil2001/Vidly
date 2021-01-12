using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Vidly.Controllers.Api
{
    public class RentalsController : ApiController
    {
        // Customer rents movies
        // 
        [HttpPost]
        public IHttpActionResult RentMovies(RentalDto rentalDto)
        {
            return Ok();
        }

    }

    public class RentalDto
    {
        public int ClientId { get; set; }
        public IEnumerable<int> MovieIds { get; set; }
    }
}

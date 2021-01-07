using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        /// <summary>
        ///  GET /api/customers
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetCustomers()
        {
            return Ok( _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>));
        }


        /// <summary>
        /// GET /api/customers/1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        /// <summary>
        /// POST /api/customers
        /// </summary>
        /// <param name="customerDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
                //throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customerDto.Id), customerDto);

            //return Mapper.Map<Customer, CustomerDto>(customer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customerDto"></param>
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();
        }



        /// <summary>
        /// DELETE /api/customers/1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return StatusCode(HttpStatusCode.NotFound);

            //// Do some business logic checks to ensure we can delete this item
            //if (!AllowedToDelete(customerInDb))
            //    return StatusCode(HttpStatusCode.Forbidden);

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}

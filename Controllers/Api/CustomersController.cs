using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }


        /// <summary>
        /// GET /api/customers/1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetCustomer(int id)
        {
            var cust = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (cust == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return cust;
        }

        /// <summary>
        /// POST /api/customers
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer"></param>
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            customerInDb.Name = customer.Name;
            customerInDb.DateOfBirth = customer.DateOfBirth;
            customerInDb.IsSubscribedToNewsletter= customer.IsSubscribedToNewsletter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;

            _context.SaveChanges();
        }



        /// <summary>
        /// DELETE /api/customers/1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            //return new HttpResponseMessage(HttpStatusCode.OK);

        }
    }
}

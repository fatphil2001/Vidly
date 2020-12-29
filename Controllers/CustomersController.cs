using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 0, Name = "Bob Smith"},
                new Customer { Id = 1, Name = "Fanny Adams"},
            };
        }

        // GET: Customers
        public ViewResult Index()
        {

            return View(GetCustomers());
        }

        //[Route("customers/{id}")]
        public ActionResult Details(int id)
        {
            var customer = GetCustomers().SingleOrDefault(cust => cust.Id == id);

            if (customer != null)
                return View(customer);
            else
                return new HttpNotFoundResult();                    
        }
    }
}
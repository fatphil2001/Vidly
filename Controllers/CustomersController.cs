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
        private CustomersViewModel customers;

        public CustomersController()
        {
            customers = new CustomersViewModel
            {
                Customers = new List<Customer>
                {
                    new Customer { Id = 0, Name = "Bob Smith"},
                    new Customer { Id = 1, Name = "Fanny Adams"},
                }
            };
        }

        // GET: Customers
        public ActionResult Index()
        {
            return View(customers);
        }

        [Route("customers/{id}")]
        public ActionResult Detail(int id)
        {
            var customer = customers.Customers.SingleOrDefault(cust => cust.Id == id);

            //if (customers.Customers.Any(cust => cust.Id == id))
            //    return View(customers.Customers.First[id]);
            if (customer != null)
                return View(customer);
            else
                return new HttpNotFoundResult();                    
        }
    }
}
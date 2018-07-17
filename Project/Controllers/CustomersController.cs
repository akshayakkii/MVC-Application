using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using Newtonsoft.Json;

namespace Project.Controllers
{
    public class CustomersController : Controller
    {
        private KeysOnboard db = new KeysOnboard();

        // Index page
        public ActionResult Index()
        {
            return View();
        }

        // Retrieves a list of all the customers from the database
        public ActionResult GetCustomers()
        {
            List<CustomerViewModel> customers = db.Customers.Select(x => new CustomerViewModel
            {
                CustomerId = x.CustomerId,
                CustomerName = x.CustomerName,
                CustomerAddress  = x.CustomerAddress

            }).ToList();

            return Json(customers, JsonRequestBehavior.AllowGet);
        }

        // Adds a customer to the database
        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // Edit the customer and save it to the database
        [HttpPost]
        public ActionResult EditCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
            }

            return null;
        }

        // Deletes a customer from the database
        [HttpPost]
        public ActionResult DeleteCustomer(int CustomerId)
        {
            Customer customer = db.Customers.Find(CustomerId);

            try
            {
                db.Customers.Remove(customer);
                db.SaveChanges();
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Foreign key relationship is being violated with ProductsSold");
            }

            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

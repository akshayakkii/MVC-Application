using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using Newtonsoft.Json;

namespace Project.Controllers
{
    public class ProductsController : Controller
    {
        private KeysOnboard db = new KeysOnboard();

        // Index page
        public ActionResult Index()
        {
            return View();
        }

        // Retrieves a list of all the products from the database
        public ActionResult GetProducts()
        {
            List<ProductViewModel> products = db.Products.Select(x => new ProductViewModel
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductPrice = x.ProductPrice

            }).ToList();

            return Json(products, JsonRequestBehavior.AllowGet);
        }

        // Adds a product to the database
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
            }

            return null;
        }

        // Edit the product and save it to the database
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }

            return null;
        }

        // Deletes a product from the database
        [HttpPost]
        public ActionResult DeleteProduct(int ProductId)
        {
            Product product = db.Products.Find(ProductId);

            try
            {
                db.Products.Remove(product);
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

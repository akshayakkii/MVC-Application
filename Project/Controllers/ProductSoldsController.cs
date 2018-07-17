using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace OnBoardingTasks.Controllers
{
    public class ProductSoldsController : Controller
    {
        private KeysOnboard db = new KeysOnboard();

        // Index page
        public ActionResult Index()
        {
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName");
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");


            return View();
        }


        // TO get a list of all the sales from the database
        public ActionResult GetSales()
        {
            List<ProductSoldViewModel> sales = db.ProductSolds.Select(x => new ProductSoldViewModel
            {
                ProductSoldID = x.ProductSoldID,
                CustomerId = x.CustomerId,
                ProductId = x.ProductId,
                StoreId = x.StoreId,
                CustomerName = x.Customer.CustomerName,
                ProductName = x.Product.ProductName,
                StoreName = x.Store.StoreName,
                DateSold = x.DateSold
            }).ToList();

            return Json(sales, JsonRequestBehavior.AllowGet);
        }



        // To add a sale to the database
        [HttpPost]
        public ActionResult AddSale(ProductSold sale)
        {
            if (ModelState.IsValid)
            {
                db.ProductSolds.Add(sale);
                db.SaveChanges();
            }

            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName");
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");


            return null;
        }


        // To edit a sale and save it to the database
        [HttpPost]
        public ActionResult EditSale(ProductSoldViewModel sale)
        {
            try
            {
                if (sale.ProductSoldID >= 0)
                {
                    ProductSold editedSale = db.ProductSolds.Find(sale.ProductSoldID);
                    editedSale.CustomerId = sale.CustomerId;
                    editedSale.StoreId = sale.StoreId;
                    editedSale.ProductId = sale.ProductId;
                    editedSale.DateSold = sale.DateSold;

                    db.Entry(editedSale).State = EntityState.Modified;
                    db.SaveChanges();

                    ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName");
                    ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName");
                    ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");


                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }


        // To delete a sale from the database
        [HttpPost]
        public ActionResult DeleteSale(int ProductSoldID)
        {
            ProductSold sale = db.ProductSolds.Find(ProductSoldID);

            try
            {
                db.ProductSolds.Remove(sale);
                db.SaveChanges();

                ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName");
                ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName");
                ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");

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
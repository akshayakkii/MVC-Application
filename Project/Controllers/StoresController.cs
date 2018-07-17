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
using System.Data.Entity.Infrastructure;

namespace Project.Controllers
{
    public class StoresController : Controller
    {
        private KeysOnboard db = new KeysOnboard();

        // Index page
        public ActionResult Index()
        {
            return View();
        }

        // Retrieves a list of all the stores from the database
        public ActionResult GetStore()
        {
            List<StoreViewModel> stores = db.Stores.Select(x => new StoreViewModel
            {
                StoreId = x.StoreId,
                StoreName = x.StoreName,
                StoreAddress = x.StoreAddress

            }).ToList();
            
            return Json(stores, JsonRequestBehavior.AllowGet);
        }

        // Adds a store to the database
        [HttpPost]
        public ActionResult AddStore(Store store)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Stores.Add(store);
                    db.SaveChanges();
                }
                catch(DbUpdateConcurrencyException ex)
                
                {
                    ex.Entries.Single().Reload();
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index"); ;
        }

        // Edit the store and save it to the database
        [HttpPost]
        public ActionResult EditStore(Store store)
        {
            if (ModelState.IsValid)
            {
                db.Entry(store).State = EntityState.Modified;
                db.SaveChanges();
            }

            return null;
        }

        // Deletes a store from the database
        [HttpPost]
        public ActionResult DeleteStore(int StoreId)
        {
            Store store = db.Stores.Find(StoreId);

            try
            {
                db.Stores.Remove(store);
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

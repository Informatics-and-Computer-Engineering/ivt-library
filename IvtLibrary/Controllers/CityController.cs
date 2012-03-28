using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IvtLibrary;

namespace IvtLibrary.Controllers
{ 
    public class CityController : Controller
    {
        private IvtLibraryEntities db = new IvtLibraryEntities();

        //
        // GET: /City/

        public ViewResult Index()
        {
            return View(db.City.ToList());
        }

        //
        // GET: /City/Details/5

        public ViewResult Details(int id)
        {
            City city = db.City.Single(c => c.id == id);
            return View(city);
        }

        //
        // GET: /City/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /City/Create

        [HttpPost]
        public ActionResult Create(City city)
        {
            if (ModelState.IsValid)
            {
                db.City.AddObject(city);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(city);
        }
        
        //
        // GET: /City/Edit/5
 
        public ActionResult Edit(int id)
        {
            City city = db.City.Single(c => c.id == id);
            return View(city);
        }

        //
        // POST: /City/Edit/5

        [HttpPost]
        public ActionResult Edit(City city)
        {
            if (ModelState.IsValid)
            {
                db.City.Attach(city);
                db.ObjectStateManager.ChangeObjectState(city, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(city);
        }

        //
        // GET: /City/Delete/5
 
        public ActionResult Delete(int id)
        {
            City city = db.City.Single(c => c.id == id);
            return View(city);
        }

        //
        // POST: /City/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            City city = db.City.Single(c => c.id == id);
            db.City.DeleteObject(city);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
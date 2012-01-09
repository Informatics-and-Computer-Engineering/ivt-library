using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryModel;

namespace IvtLibrary.Controllers
{ 
    public class ScaleController : Controller
    {
        private LibraryEntities db = new LibraryEntities();

        //
        // GET: /Scale/

        public ViewResult Index()
        {
            return View(db.Scales.ToList());
        }

        //
        // GET: /Scale/Details/5

        public ViewResult Details(int id)
        {
            Scale scale = db.Scales.Single(s => s.Id == id);
            return View(scale);
        }

        //
        // GET: /Scale/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Scale/Create

        [HttpPost]
        public ActionResult Create(Scale scale)
        {
            if (ModelState.IsValid)
            {
                db.Scales.AddObject(scale);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(scale);
        }
        
        //
        // GET: /Scale/Edit/5
 
        public ActionResult Edit(int id)
        {
            Scale scale = db.Scales.Single(s => s.Id == id);
            return View(scale);
        }

        //
        // POST: /Scale/Edit/5

        [HttpPost]
        public ActionResult Edit(Scale scale)
        {
            if (ModelState.IsValid)
            {
                db.Scales.Attach(scale);
                db.ObjectStateManager.ChangeObjectState(scale, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(scale);
        }

        //
        // GET: /Scale/Delete/5
 
        public ActionResult Delete(int id)
        {
            Scale scale = db.Scales.Single(s => s.Id == id);
            return View(scale);
        }

        //
        // POST: /Scale/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Scale scale = db.Scales.Single(s => s.Id == id);
            db.Scales.DeleteObject(scale);
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
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
    public class ResearchController : Controller
    {
        private LibraryEntities db = new LibraryEntities();

        //
        // GET: /Research/

        public ViewResult Index()
        {
            return View(db.Researches.ToList());
        }

        //
        // GET: /Research/Details/5

        public ViewResult Details(int id)
        {
            Research research = db.Researches.Single(r => r.Id == id);
            return View(research);
        }

        //
        // GET: /Research/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Research/Create

        [HttpPost]
        public ActionResult Create(Research research)
        {
            if (ModelState.IsValid)
            {
                db.Researches.AddObject(research);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(research);
        }
        
        //
        // GET: /Research/Edit/5
 
        public ActionResult Edit(int id)
        {
            Research research = db.Researches.Single(r => r.Id == id);
            return View(research);
        }

        //
        // POST: /Research/Edit/5

        [HttpPost]
        public ActionResult Edit(Research research)
        {
            if (ModelState.IsValid)
            {
                db.Researches.Attach(research);
                db.ObjectStateManager.ChangeObjectState(research, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(research);
        }

        //
        // GET: /Research/Delete/5
 
        public ActionResult Delete(int id)
        {
            Research research = db.Researches.Single(r => r.Id == id);
            return View(research);
        }

        //
        // POST: /Research/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Research research = db.Researches.Single(r => r.Id == id);
            db.Researches.DeleteObject(research);
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
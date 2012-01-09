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
    public class ConferenceController : Controller
    {
        private LibraryEntities db = new LibraryEntities();

        //
        // GET: /Conference/

        public ViewResult Index()
        {
            var conferences = db.Conferences.Include("Scale");
            return View(conferences.ToList());
        }

        //
        // GET: /Conference/Details/5

        public ViewResult Details(int id)
        {
            Conference conference = db.Conferences.Single(c => c.Id == id);
            return View(conference);
        }

        //
        // GET: /Conference/Create

        public ActionResult Create()
        {
            ViewBag.ScaleId = new SelectList(db.Scales, "Id", "Name");
            return View();
        } 

        //
        // POST: /Conference/Create

        [HttpPost]
        public ActionResult Create(Conference conference)
        {
            if (ModelState.IsValid)
            {
                db.Conferences.AddObject(conference);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ScaleId = new SelectList(db.Scales, "Id", "Name", conference.ScaleId);
            return View(conference);
        }
        
        //
        // GET: /Conference/Edit/5
 
        public ActionResult Edit(int id)
        {
            Conference conference = db.Conferences.Single(c => c.Id == id);
            ViewBag.ScaleId = new SelectList(db.Scales, "Id", "Name", conference.ScaleId);
            return View(conference);
        }

        //
        // POST: /Conference/Edit/5

        [HttpPost]
        public ActionResult Edit(Conference conference)
        {
            if (ModelState.IsValid)
            {
                db.Conferences.Attach(conference);
                db.ObjectStateManager.ChangeObjectState(conference, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ScaleId = new SelectList(db.Scales, "Id", "Name", conference.ScaleId);
            return View(conference);
        }

        //
        // GET: /Conference/Delete/5
 
        public ActionResult Delete(int id)
        {
            Conference conference = db.Conferences.Single(c => c.Id == id);
            return View(conference);
        }

        //
        // POST: /Conference/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Conference conference = db.Conferences.Single(c => c.Id == id);
            db.Conferences.DeleteObject(conference);
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
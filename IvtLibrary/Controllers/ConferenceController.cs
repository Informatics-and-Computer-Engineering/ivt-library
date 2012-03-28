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
    public class ConferenceController : Controller
    {
        private IvtLibraryEntities db = new IvtLibraryEntities();

        //
        // GET: /Conference/

        public ViewResult Index()
        {
            var conference = db.Conference.Include("Scale");
            return View(conference.ToList());
        }

        //
        // GET: /Conference/Details/5

        public ViewResult Details(int id)
        {
            Conference conference = db.Conference.Single(c => c.id == id);
            return View(conference);
        }

        //
        // GET: /Conference/Create

        public ActionResult Create()
        {
            ViewBag.scale_id = new SelectList(db.Scale, "id", "name");
            return View();
        } 

        //
        // POST: /Conference/Create

        [HttpPost]
        public ActionResult Create(Conference conference)
        {
            if (ModelState.IsValid)
            {
                db.Conference.AddObject(conference);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.scale_id = new SelectList(db.Scale, "id", "name", conference.scale_id);
            return View(conference);
        }
        
        //
        // GET: /Conference/Edit/5
 
        public ActionResult Edit(int id)
        {
            Conference conference = db.Conference.Single(c => c.id == id);
            ViewBag.scale_id = new SelectList(db.Scale, "id", "name", conference.scale_id);
            return View(conference);
        }

        //
        // POST: /Conference/Edit/5

        [HttpPost]
        public ActionResult Edit(Conference conference)
        {
            if (ModelState.IsValid)
            {
                db.Conference.Attach(conference);
                db.ObjectStateManager.ChangeObjectState(conference, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.scale_id = new SelectList(db.Scale, "id", "name", conference.scale_id);
            return View(conference);
        }

        //
        // GET: /Conference/Delete/5
 
        public ActionResult Delete(int id)
        {
            Conference conference = db.Conference.Single(c => c.id == id);
            return View(conference);
        }

        //
        // POST: /Conference/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Conference conference = db.Conference.Single(c => c.id == id);
            db.Conference.DeleteObject(conference);
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
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
    public class ThemeController : Controller
    {
        private LibraryEntities db = new LibraryEntities();

        //
        // GET: /Theme/

        public ViewResult Index()
        {
            return View(db.Themes.ToList());
        }

        //
        // GET: /Theme/Details/5

        public ViewResult Details(int id)
        {
            Theme theme = db.Themes.Single(t => t.Id == id);
            return View(theme);
        }

        //
        // GET: /Theme/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Theme/Create

        [HttpPost]
        public ActionResult Create(Theme theme)
        {
            if (ModelState.IsValid)
            {
                db.Themes.AddObject(theme);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(theme);
        }
        
        //
        // GET: /Theme/Edit/5
 
        public ActionResult Edit(int id)
        {
            Theme theme = db.Themes.Single(t => t.Id == id);
            return View(theme);
        }

        //
        // POST: /Theme/Edit/5

        [HttpPost]
        public ActionResult Edit(Theme theme)
        {
            if (ModelState.IsValid)
            {
                db.Themes.Attach(theme);
                db.ObjectStateManager.ChangeObjectState(theme, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(theme);
        }

        //
        // GET: /Theme/Delete/5
 
        public ActionResult Delete(int id)
        {
            Theme theme = db.Themes.Single(t => t.Id == id);
            return View(theme);
        }

        //
        // POST: /Theme/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Theme theme = db.Themes.Single(t => t.Id == id);
            db.Themes.DeleteObject(theme);
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
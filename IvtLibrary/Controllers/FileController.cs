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
    public class FileController : Controller
    {
        private LibraryEntities db = new LibraryEntities();

        //
        // GET: /File/

        public ViewResult Index()
        {
            var files = db.Files.Include("Type");
            return View(files.ToList());
        }

        //
        // GET: /File/Details/5

        public ViewResult Details(int id)
        {
            File file = db.Files.Single(f => f.Id == id);
            return View(file);
        }

        //
        // GET: /File/Create

        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name");
            return View();
        } 

        //
        // POST: /File/Create

        [HttpPost]
        public ActionResult Create(File file)
        {
            if (ModelState.IsValid)
            {
                db.Files.AddObject(file);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", file.TypeId);
            return View(file);
        }
        
        //
        // GET: /File/Edit/5
 
        public ActionResult Edit(int id)
        {
            File file = db.Files.Single(f => f.Id == id);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", file.TypeId);
            return View(file);
        }

        //
        // POST: /File/Edit/5

        [HttpPost]
        public ActionResult Edit(File file)
        {
            if (ModelState.IsValid)
            {
                db.Files.Attach(file);
                db.ObjectStateManager.ChangeObjectState(file, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", file.TypeId);
            return View(file);
        }

        //
        // GET: /File/Delete/5
 
        public ActionResult Delete(int id)
        {
            File file = db.Files.Single(f => f.Id == id);
            return View(file);
        }

        //
        // POST: /File/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            File file = db.Files.Single(f => f.Id == id);
            db.Files.DeleteObject(file);
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
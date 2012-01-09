﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryModel;
using Type = LibraryModel.Type;

namespace IvtLibrary.Controllers
{ 
    public class TypeController : Controller
    {
        private LibraryEntities db = new LibraryEntities();

        //
        // GET: /Type/

        public ViewResult Index()
        {
            return View(db.Types.ToList());
        }

        //
        // GET: /Type/Details/5

        public ViewResult Details(int id)
        {
            Type type = db.Types.Single(t => t.Id == id);
            return View(type);
        }

        //
        // GET: /Type/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Type/Create

        [HttpPost]
        public ActionResult Create(Type type)
        {
            if (ModelState.IsValid)
            {
                db.Types.AddObject(type);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(type);
        }
        
        //
        // GET: /Type/Edit/5
 
        public ActionResult Edit(int id)
        {
            Type type = db.Types.Single(t => t.Id == id);
            return View(type);
        }

        //
        // POST: /Type/Edit/5

        [HttpPost]
        public ActionResult Edit(Type type)
        {
            if (ModelState.IsValid)
            {
                db.Types.Attach(type);
                db.ObjectStateManager.ChangeObjectState(type, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(type);
        }

        //
        // GET: /Type/Delete/5
 
        public ActionResult Delete(int id)
        {
            Type type = db.Types.Single(t => t.Id == id);
            return View(type);
        }

        //
        // POST: /Type/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Type type = db.Types.Single(t => t.Id == id);
            db.Types.DeleteObject(type);
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
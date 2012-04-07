using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IvtLibrary;

namespace IvtLibrary.Controllers
{ 
    public class FileController : Controller
    {
        private IvtLibraryEntities db = new IvtLibraryEntities();

        //
        // GET: /File/

        public ViewResult Index()
        {
            var file = db.File.Include("Type");
            return View(file.ToList());
        }

        //
        // GET: /File/Details/5

        public ViewResult Details(int id)
        {
            File file = db.File.Single(f => f.id == id);
            return View(file);
        }

        //
        // GET: /File/Create

        public ActionResult Create()
        {
            ViewBag.type_id = new SelectList(db.Type, "id", "name");
            return View();
        } 

        //
        // POST: /File/Create

        [HttpPost]
        public ActionResult Create(File file)
        {
            if (ModelState.IsValid)
            {
                byte[] fileData = new byte[0];
                foreach (string fileName in Request.Files)
                {
                    file.content_type = Request.Files[fileName].ContentType;
                    Stream stream = Request.Files[fileName].InputStream;
                    fileData = new byte[stream.Length];
                    stream.Read(fileData, 0, (int)stream.Length);
                }
                file.data = fileData;
                db.File.AddObject(file);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.type_id = new SelectList(db.Type, "id", "name", file.type_id);
            return View(file);
        }
        
        //
        // GET: /File/Edit/5
 
        public ActionResult Edit(int id)
        {
            File file = db.File.Single(f => f.id == id);
            byte[] fileData = new byte[0];
            foreach (string fileName in Request.Files)
            {
                file.content_type = Request.Files[fileName].ContentType;
                Stream stream = Request.Files[fileName].InputStream;
                fileData = new byte[stream.Length];
                stream.Read(fileData, 0, (int)stream.Length);
            }
            file.data = fileData;
            ViewBag.type_id = new SelectList(db.Type, "id", "name", file.type_id);
            return View(file);
        }

        //
        // POST: /File/Edit/5

        [HttpPost]
        public ActionResult Edit(File file)
        {
            if (ModelState.IsValid)
            {
                byte[] fileData = new byte[0];
                foreach (string fileName in Request.Files)
                {
                    file.content_type = Request.Files[fileName].ContentType;
                    Stream stream = Request.Files[fileName].InputStream;
                    fileData = new byte[stream.Length];
                    stream.Read(fileData, 0, (int)stream.Length);
                }
                file.data = fileData;
                db.File.Attach(file);
                db.ObjectStateManager.ChangeObjectState(file, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.type_id = new SelectList(db.Type, "id", "name", file.type_id);
            return View(file);
        }

        //
        // GET: /File/Delete/5
 
        public ActionResult Delete(int id)
        {
            File file = db.File.Single(f => f.id == id);
            return View(file);
        }

        //
        // POST: /File/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            File file = db.File.Single(f => f.id == id);
            db.File.DeleteObject(file);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Download(int id)
        {
            File file = db.File.Single(f => f.id == id);
            FileResult result = new FileContentResult(file.data.ToArray(), file.content_type);
            result.FileDownloadName = file.name;
            return result;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
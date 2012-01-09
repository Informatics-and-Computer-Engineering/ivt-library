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
    public class ArticleController : Controller
    {
        private LibraryEntities db = new LibraryEntities();

        //
        // GET: /Article/

        public ViewResult Index()
        {
            var articles = db.Articles.Include("City").Include("Conference");
            return View(articles.ToList());
        }

        //
        // GET: /Article/Details/5

        public ViewResult Details(int id)
        {
            Article article = db.Articles.Single(a => a.Id == id);
            return View(article);
        }

        //
        // GET: /Article/Create

        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name");
            ViewBag.ConferenceId = new SelectList(db.Conferences, "Id", "Name");
            return View();
        } 

        //
        // POST: /Article/Create

        [HttpPost]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.AddObject(article);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", article.CityId);
            ViewBag.ConferenceId = new SelectList(db.Conferences, "Id", "Name", article.ConferenceId);
            return View(article);
        }
        
        //
        // GET: /Article/Edit/5
 
        public ActionResult Edit(int id)
        {
            Article article = db.Articles.Single(a => a.Id == id);
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", article.CityId);
            ViewBag.ConferenceId = new SelectList(db.Conferences, "Id", "Name", article.ConferenceId);
            return View(article);
        }

        //
        // POST: /Article/Edit/5

        [HttpPost]
        public ActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Attach(article);
                db.ObjectStateManager.ChangeObjectState(article, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", article.CityId);
            ViewBag.ConferenceId = new SelectList(db.Conferences, "Id", "Name", article.ConferenceId);
            return View(article);
        }

        //
        // GET: /Article/Delete/5
 
        public ActionResult Delete(int id)
        {
            Article article = db.Articles.Single(a => a.Id == id);
            return View(article);
        }

        //
        // POST: /Article/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Article article = db.Articles.Single(a => a.Id == id);
            db.Articles.DeleteObject(article);
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
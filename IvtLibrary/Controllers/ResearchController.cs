using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IvtLibrary;
using IvtLibrary.Models;

namespace IvtLibrary.Controllers
{ 
    public class ResearchController : Controller
    {
        private IvtLibraryEntities db = new IvtLibraryEntities();
        private AuthorRepository authorRepository = new AuthorRepository();
        private ThemeRepository themeRepository = new ThemeRepository();

        //
        // GET: /Research/

        public ViewResult Index()
        {
            return View(db.Research.ToList());
        }

        //
        // GET: /Research/Details/5

        public ViewResult Details(int id)
        {
            Research research = db.Research.Single(r => r.id == id);
            return View(research);
        }

        //
        // GET: /Research/Create

        public ActionResult Create()
        {
            ViewBag.AuthorsList = authorRepository.FillAuthorsCheckBoxList(null);
            ViewBag.ThemesList = themeRepository.FillThemesCheckBoxList(null);
            return View();
        } 

        //
        // POST: /Research/Create

        [HttpPost]
        public ActionResult Create(Research research, int[] themeIds, int[] authorIds)
        {
            if (ModelState.IsValid)
            {
                db.Research.AddObject(research);
                SetResearchAuthors(research, authorIds);
                SetResearchThemes(research, themeIds);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(research);
        }
        
        //
        // GET: /Research/Edit/5
 
        public ActionResult Edit(int id)
        {
            Research research = db.Research.Single(r => r.id == id);
            ViewBag.AuthorsList = authorRepository.FillAuthorsCheckBoxList(research.Author);
            ViewBag.ThemesList = themeRepository.FillThemesCheckBoxList(research.Theme);
            return View(research);
        }

        //
        // POST: /Research/Edit/5

        [HttpPost]
        public ActionResult Edit(Research research, int[] themeIds, int[] authorIds)
        {
            if (ModelState.IsValid)
            {
                db.Research.Attach(research);
                SetResearchAuthors(research, authorIds);
                SetResearchThemes(research, themeIds);
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
            Research research = db.Research.Single(r => r.id == id);
            return View(research);
        }

        //
        // POST: /Research/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Research research = db.Research.Single(r => r.id == id);
            research.Theme.Clear();
            research.Author.Clear();
            db.Research.DeleteObject(research);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region Theme connection

        private void SetResearchThemes(Research research, IEnumerable<int> themeIds)
        {
            // получаем коллекцию тем, выбранных пользователем на форме
            var selectedThemes = db.Theme.Where(t => themeIds.Contains(t.id));
            // очищаем список старых тем
            research.Theme.Clear();
            // заполняем список тем теми которые выбрал пользователь
            if (themeIds != null)
            {
                foreach (var theme in selectedThemes)
                {
                    research.Theme.Add(theme);
                }
            }
        }

        #endregion

        #region Author connection

        private void SetResearchAuthors(Research research, IEnumerable<int> authorIds)
        {
            // получаем коллекцию авторов, выбранных пользователем на форме
            var selectedAuthors = db.Author.Where(t => authorIds.Contains(t.id));
            // очищаем список старых авторов
            research.Author.Clear();
            // заполняем список авторов теми которых выбрал пользователь
            if (authorIds != null)
            {
                foreach (var author in selectedAuthors)
                {
                    research.Author.Add(author);
                }
            }
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Web.Mvc;
using IvtLibrary.Models;

namespace IvtLibrary.Controllers
{ 
    public class ResearchController : Controller
    {
        private readonly IvtLibraryEntities db = new IvtLibraryEntities();
        private readonly AuthorRepository authorRepository;
        private readonly ThemeRepository themeRepository;

        public ResearchController()
        {
            authorRepository = new AuthorRepository(db);
            themeRepository = new ThemeRepository(db);
        }

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
                SetResearchAuthors(research.Author, authorIds);
                SetResearchThemes(research.Theme, themeIds);
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
                SetResearchAuthors(research.Author, authorIds);
                SetResearchThemes(research.Theme, themeIds);
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

        #region Author connection

        private void SetResearchAuthors(EntityCollection<Author> authors, IEnumerable<int> authorIds)
        {
            // получаем коллекцию авторов, выбранных пользователем на форме
            var selectedAuthors = db.Author.Where(t => authorIds.Contains(t.id));
            // очищаем список старых авторов
            authors.Clear();
            // заполняем список авторов теми которых выбрал пользователь
            if (authorIds != null)
            {
                foreach (var author in selectedAuthors)
                {
                    authors.Add(author);
                }
            }
        }

        #endregion

        #region Theme connection

        private void SetResearchThemes(EntityCollection<Theme> themes, IEnumerable<int> themeIds)
        {
            // получаем коллекцию тем, выбранных пользователем на форме
            var selectedThemes = db.Theme.Where(t => themeIds.Contains(t.id));
            // очищаем список старых тем
            themes.Clear();
            // заполняем список тем теми которые выбрал пользователь
            if (themeIds != null)
            {
                foreach (var theme in selectedThemes)
                {
                    themes.Add(theme);
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
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using IvtLibrary.Models;

namespace IvtLibrary.Controllers
{ 
    public class AuthorController : Controller
    {
        private IvtLibraryEntities db = new IvtLibraryEntities();
        private ThemeRepository themeRepository = new ThemeRepository();

        //
        // GET: /Author/

        public ViewResult Index()
        {
            return View(db.Author.ToList());
        }

        //
        // GET: /Author/Details/5

        public ViewResult Details(int id)
        {
            Author author = db.Author.Single(a => a.id == id);
            return View(author);
        }

        //
        // GET: /Author/Create

        public ActionResult Create()
        {
            ViewBag.ThemesList = themeRepository.FillThemesCheckBoxList(null);
            return View();
        } 

        //
        // POST: /Author/Create

        [HttpPost]
        public ActionResult Create(Author author, int[] themeIds)
        {
            if (ModelState.IsValid)
            {
                db.Author.AddObject(author);
                SetAuthorThemes(author, themeIds);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            return View(author);
        }
        
        //
        // GET: /Author/Edit/5
 
        public ActionResult Edit(int id)
        {
            Author author = db.Author.Single(a => a.id == id);
            ViewBag.ThemesList = themeRepository.FillThemesCheckBoxList(author.Theme);
            return View(author);
        }

        //
        // POST: /Author/Edit/5

        [HttpPost]
        public ActionResult Edit(Author author, int[] themeIds)
        {
            if (ModelState.IsValid)
            {
                db.Author.Attach(author);
                SetAuthorThemes(author, themeIds);
                db.ObjectStateManager.ChangeObjectState(author, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        //
        // GET: /Author/Delete/5
 
        public ActionResult Delete(int id)
        {
            Author author = db.Author.Single(a => a.id == id);
            return View(author);
        }

        //
        // POST: /Author/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Author author = db.Author.Single(a => a.id == id);
            author.Theme.Clear();
            db.Author.DeleteObject(author);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region Theme connection

        private void SetAuthorThemes(Author author, IEnumerable<int> themeIds)
        {
            // получаем коллекцию тем, выбранных пользователем на форме
            var selectedThemes = db.Theme.Where(t => themeIds.Contains(t.id));
            // очищаем список старых тем
            author.Theme.Clear();
            // заполняем список тем теми которые выбрал пользователь
            if (themeIds != null)
            {
                foreach (var theme in selectedThemes)
                {
                    author.Theme.Add(theme);
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
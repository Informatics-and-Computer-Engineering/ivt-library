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
    public class BookController : Controller
    {
        private IvtLibraryEntities db = new IvtLibraryEntities();
        private AuthorRepository authorRepository = new AuthorRepository();
        private ThemeRepository themeRepository = new ThemeRepository();

        //
        // GET: /Book/

        public ViewResult Index()
        {
            return View(db.Book.ToList());
        }

        //
        // GET: /Book/Details/5

        public ViewResult Details(int id)
        {
            Book book = db.Book.Single(b => b.id == id);
            return View(book);
        }

        //
        // GET: /Book/Create

        public ActionResult Create()
        {
            ViewBag.AuthorsList = authorRepository.FillAuthorsCheckBoxList(null);
            ViewBag.ThemesList = themeRepository.FillThemesCheckBoxList(null);
            return View();
        } 

        //
        // POST: /Book/Create

        [HttpPost]
        public ActionResult Create(Book book, int[] authorIds, int[] themeIds)
        {
            if (ModelState.IsValid)
            {
                db.Book.AddObject(book);
                SetBookAuthors(book, authorIds);
                SetBookThemes(book, themeIds);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            return View(book);
        }
        
        //
        // GET: /Book/Edit/5
 
        public ActionResult Edit(int id)
        {
            Book book = db.Book.Single(b => b.id == id);
            ViewBag.AuthorsList = authorRepository.FillAuthorsCheckBoxList(book.Author);
            ViewBag.ThemesList = themeRepository.FillThemesCheckBoxList(book.Theme);
            return View(book);
        }

        //
        // POST: /Book/Edit/5

        [HttpPost]
        public ActionResult Edit(Book book, int[] authorIds, int[] themeIds)
        {
            if (ModelState.IsValid)
            {
                db.Book.Attach(book);
                SetBookAuthors(book, authorIds);
                SetBookThemes(book, themeIds);
                db.ObjectStateManager.ChangeObjectState(book, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        //
        // GET: /Book/Delete/5
 
        public ActionResult Delete(int id)
        {
            Book book = db.Book.Single(b => b.id == id);
            return View(book);
        }

        //
        // POST: /Book/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Book book = db.Book.Single(b => b.id == id);
            book.Author.Clear();
            book.Theme.Clear();
            db.Book.DeleteObject(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region Author connection

        private void SetBookAuthors(Book book, IEnumerable<int> authorIds)
        {
            // получаем коллекцию авторов, выбранных пользователем на форме
            var selectedAuthors = db.Author.Where(t => authorIds.Contains(t.id));
            // очищаем список старых авторов
            book.Author.Clear();
            // заполняем список авторов теми которых выбрал пользователь
            if (authorIds != null)
            {
                foreach (var author in selectedAuthors)
                {
                    book.Author.Add(author);
                }
            }
        }

        #endregion

        #region Theme connection

        private void SetBookThemes(Book book, IEnumerable<int> themeIds)
        {
            // получаем коллекцию тем, выбранных пользователем на форме
            var selectedThemes = db.Theme.Where(t => themeIds.Contains(t.id));
            // очищаем список старых тем
            book.Theme.Clear();
            // заполняем список тем теми которые выбрал пользователь
            if (themeIds != null)
            {
                foreach (var theme in selectedThemes)
                {
                    book.Theme.Add(theme);
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
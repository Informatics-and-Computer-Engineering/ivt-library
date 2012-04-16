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
    public class BookController : Controller
    {
        private IvtLibraryEntities db = new IvtLibraryEntities();

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
            FillAuthorsCheckBoxList(null);
            FillThemesCheckBoxList(null);
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
            FillAuthorsCheckBoxList(book);
            FillThemesCheckBoxList(book);
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
            db.Book.DeleteObject(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region Author connection

        private IList<Author> GetAuthorsByIds(int[] authorIds)
        {
            List<Author> selectedAuthors = new List<Author>();
            //выбираем все темы с заданными id
            foreach (int authorId in authorIds)
            {
                Author author = db.Author.Single(t => t.id == authorId);
                selectedAuthors.Add(author);
            }
            return selectedAuthors;
        }

        private void SetBookAuthors(Book book, int[] authorIds)
        {
            // получаем коллекцию тем, выбранных пользователем на форме
            var selectedAuthors = GetAuthorsByIds(authorIds);
            // Вытаскиваем темы которые уже есть у данного автора
            var bookAuthors = book.Author.ToList();
            // удаляем темы которые исчезли из отмеченных
            foreach (var author in bookAuthors)
            {
                if (!selectedAuthors.Contains(author))
                {
                    book.Author.Remove(author);
                }
            }
            // добавляем темы которые добавил пользователь
            foreach (var author in selectedAuthors)
            {
                if (!bookAuthors.Contains(author))
                {
                    book.Author.Add(author);
                }
            }

        }

        // заполняет список чекбоксов тем
        private void FillAuthorsCheckBoxList(Book book)
        {
            // получаем список тем, привязанных к автору, если он есть
            HashSet<int> authors;
            if (book != null)
            {
                authors = new HashSet<int>(book.Author.Select(c => c.id));
            }
            else
            {
                authors = new HashSet<int>();
            }
            var allAuthors = db.Author;
            var authorsCheckBoxList = new List<SelectListItem>();
            foreach (var author in allAuthors)
            {
                authorsCheckBoxList.Add(new SelectListItem
                {
                    Value = author.id.ToString(),
                    Text = author.first_name + " " + author.middle_name + " " + author.last_name,
                    Selected = authors.Contains(author.id)
                });
            }
            ViewBag.AuthorsList = authorsCheckBoxList;
        }
        #endregion

        #region Theme connection

        private void SetBookThemes(Book book, int[] themeIds)
        {
            // получаем коллекцию тем, выбранных пользователем на форме
            var selectedThemes = GetThemesByIds(themeIds);
            // Вытаскиваем темы которые уже есть у данного автора
            var bookThemes = book.Theme.ToList();
            // удаляем темы которые исчезли из отмеченных
            foreach (var theme in bookThemes)
            {
                if (!selectedThemes.Contains(theme))
                {
                    book.Theme.Remove(theme);
                }
            }
            // добавляем темы которые добавил пользователь
            foreach (var theme in selectedThemes)
            {
                if (!bookThemes.Contains(theme))
                {
                    book.Theme.Add(theme);
                }
            }

        }

        private IList<Theme> GetThemesByIds(int[] themeIds)
        {
            List<Theme> selectedThemes = new List<Theme>();
            //выбираем все темы с заданными id
            foreach (int themeId in themeIds)
            {
                Theme theme = db.Theme.Single(t => t.id == themeId);
                selectedThemes.Add(theme);
            }
            return selectedThemes;
        }

        // заполняет список чекбоксов тем
        private void FillThemesCheckBoxList(Book book)
        {
            // получаем список тем, привязанных к автору, если он есть
            HashSet<int> themes;
            if (book != null)
            {
                themes = new HashSet<int>(book.Theme.Select(c => c.id));
            }
            else
            {
                themes = new HashSet<int>();
            }
            var allThemes = db.Theme;
            var themesCheckBoxList = new List<SelectListItem>();
            foreach (var theme in allThemes)
            {
                themesCheckBoxList.Add(new SelectListItem
                {
                    Value = theme.id.ToString(),
                    Text = theme.name,
                    Selected = themes.Contains(theme.id)
                });
            }
            ViewBag.ThemesList = themesCheckBoxList;
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
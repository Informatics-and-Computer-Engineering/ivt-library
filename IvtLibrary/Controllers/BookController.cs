using System.Collections.Generic;
using System.Data;
using System.Data.Objects.DataClasses;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using IvtLibrary.Models;

namespace IvtLibrary.Controllers
{ 
    public class BookController : Controller
    {
        private readonly IvtLibraryEntities db = new IvtLibraryEntities();
        private readonly AuthorRepository authorRepository;
        private readonly ThemeRepository themeRepository;

        public BookController()
        {
            authorRepository = new AuthorRepository(db);
            themeRepository = new ThemeRepository(db);
        }

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
        public ActionResult Create(Book book, int[] authorIds, int[] themeIds, string fileName)
        {
            if (ModelState.IsValid)
            {
                FileBook file = new FileBook {type_id = db.Type.Single(t => t.name == "Книга").id, name = fileName};
                var fileElement = Request.Files[0];
                file.content_type = fileElement.ContentType;
                Stream stream = fileElement.InputStream;
                byte[] fileData = new byte[stream.Length];
                stream.Read(fileData, 0, (int)stream.Length);
                file.data = fileData;
                file.book_id = book.id;
                db.FileBook.AddObject(file);
                db.Book.AddObject(book);
                book.FileBook.Add(file);
                SetBookAuthors(book.Author, authorIds);
                SetBookThemes(book.Theme, themeIds);
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
                SetBookAuthors(book.Author, authorIds);
                SetBookThemes(book.Theme, themeIds);
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

        private void SetBookAuthors(EntityCollection<Author> authors, IEnumerable<int> authorIds)
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

        private void SetBookThemes(EntityCollection<Theme> themes, IEnumerable<int> themeIds)
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
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
    public class ArticleController : Controller
    {
        private IvtLibraryEntities db = new IvtLibraryEntities();

        //
        // GET: /Article/

        public ViewResult Index()
        {
            var article = db.Article.Include("City").Include("Conference");
            return View(article.ToList());
        }

        //
        // GET: /Article/Details/5

        public ViewResult Details(int id)
        {
            Article article = db.Article.Single(a => a.id == id);
            return View(article);
        }

        //
        // GET: /Article/Create

        public ActionResult Create()
        {
            FillAuthorsCheckBoxList(null);
            ViewBag.city_id = new SelectList(db.City, "id", "name");
            ViewBag.conference_id = new SelectList(db.Conference, "id", "name");
            return View();
        } 

        //
        // POST: /Article/Create

        [HttpPost]
        public ActionResult Create(Article article, int[] authorIds)
        {
            if (ModelState.IsValid)
            {
                db.Article.AddObject(article);
                SetArticleAuthors(article, authorIds);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.city_id = new SelectList(db.City, "id", "name", article.city_id);
            ViewBag.conference_id = new SelectList(db.Conference, "id", "name", article.conference_id);
            return View(article);
        }

        //
        // GET: /Article/Edit/5

        public ActionResult Edit(int id)
        {
            Article article = db.Article.Single(a => a.id == id);
            FillAuthorsCheckBoxList(article);
            ViewBag.city_id = new SelectList(db.City, "id", "name", article.city_id);
            ViewBag.conference_id = new SelectList(db.Conference, "id", "name", article.conference_id);
            return View(article);
        }

        //
        // POST: /Article/Edit/5

        [HttpPost]
        public ActionResult Edit(Article article, int[] authorIds)
        {
            if (ModelState.IsValid)
            {
                db.Article.Attach(article);
                SetArticleAuthors(article, authorIds);
                db.ObjectStateManager.ChangeObjectState(article, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.city_id = new SelectList(db.City, "id", "name", article.city_id);
            ViewBag.conference_id = new SelectList(db.Conference, "id", "name", article.conference_id);
            return View(article);
        }

        //
        // GET: /Article/Delete/5
 
        public ActionResult Delete(int id)
        {
            Article article = db.Article.Single(a => a.id == id);
            return View(article);
        }

        //
        // POST: /Article/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Article article = db.Article.Single(a => a.id == id);
            db.Article.DeleteObject(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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

        private void SetArticleAuthors(Article article, int[] authorIds)
        {
            // получаем коллекцию тем, выбранных пользователем на форме
            var selectedAuthors = GetAuthorsByIds(authorIds);
            // Вытаскиваем темы которые уже есть у данного автора
            var articleAuthors = article.Author.ToList();
            // удаляем темы которые исчезли из отмеченных
            foreach (var author in articleAuthors)
            {
                if (!selectedAuthors.Contains(author))
                {
                    article.Author.Remove(author);
                }
            }
            // добавляем темы которые добавил пользователь
            foreach (var author in selectedAuthors)
            {
                if (!articleAuthors.Contains(author))
                {
                    article.Author.Add(author);
                }
            }

        }

        // заполняет список чекбоксов тем
        private void FillAuthorsCheckBoxList(Article article)
        {
            // получаем список тем, привязанных к автору, если он есть
            HashSet<int> authors;
            if (article != null)
            {
                authors = new HashSet<int>(article.Author.Select(c => c.id));
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
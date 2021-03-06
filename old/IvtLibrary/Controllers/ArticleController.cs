﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using IvtLibrary.Models;

namespace IvtLibrary.Controllers
{ 
    public class ArticleController : Controller
    {
        private readonly IvtLibraryEntities db = new IvtLibraryEntities();
        private readonly AuthorRepository authorRepository;
        private readonly ThemeRepository themeRepository;

        public ArticleController()
        {
            authorRepository = new AuthorRepository(db);
            themeRepository = new ThemeRepository(db);
        }

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
            ViewBag.types = new SelectList(db.Type, "id", "name");
            ViewBag.AuthorsList = authorRepository.FillAuthorsCheckBoxList(null);
            ViewBag.ThemesList = themeRepository.FillThemesCheckBoxList(null);
            ViewBag.city_id = new SelectList(db.City, "id", "name");
            ViewBag.conference_id = new SelectList(db.Conference, "id", "name");
            ViewBag.supervizor_id = new SelectList(db.Author, "id", "last_name");
            return View();
        } 

        //
        // POST: /Article/Create

        [HttpPost]
        public ActionResult Create(Article article, int[] authorIds, int[] themeIds, string[] fileName, int[] typeId, int[] version)
        {
            if (ModelState.IsValid)
            {
                db.Article.AddObject(article);

                for(int i =0; i < fileName.Length;i++)
                {
                    FileArticle file = new FileArticle {type_id = typeId[i], name = fileName[i], version = version[i]};
                    var fileElement = Request.Files[i];
                    file.content_type = fileElement.ContentType;
                    file.article_id = article.id;
                    Stream stream = fileElement.InputStream;
                    byte[] fileData = new byte[stream.Length];
                    stream.Read(fileData, 0, (int)stream.Length);
                    file.data = fileData;
                    db.FileArticle.AddObject(file);
                    article.FileArticle.Add(file);
                }
                
                SetArticleAuthors(article.Author, authorIds);
                SetArticleThemes(article.Theme, themeIds);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        //
        // GET: /Article/Edit/5

        public ActionResult Edit(int id)
        {
            Article article = db.Article.Single(a => a.id == id);
            ViewBag.AuthorsList = authorRepository.FillAuthorsCheckBoxList(article.Author);
            ViewBag.ThemesList = themeRepository.FillThemesCheckBoxList(article.Theme);
            ViewBag.city_id = new SelectList(db.City, "id", "name", article.city_id);
            ViewBag.conference_id = new SelectList(db.Conference, "id", "name", article.conference_id);
            ViewBag.supervizor_id = new SelectList(db.Author, "id", "last_name");
            return View(article);
        }

        //
        // POST: /Article/Edit/5

        [HttpPost]
        public ActionResult Edit(Article article, int[] authorIds, int[] themeIds)
        {
            if (ModelState.IsValid)
            {
                db.Article.Attach(article);
                SetArticleAuthors(article.Author, authorIds);
                SetArticleThemes(article.Theme, themeIds);
                db.ObjectStateManager.ChangeObjectState(article, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
            article.Author.Clear();
            article.Theme.Clear();
            db.Article.DeleteObject(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region Author connection

        private void SetArticleAuthors(EntityCollection<Author> authors, IEnumerable<int> authorIds)
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

        private void SetArticleThemes(EntityCollection<Theme> themes, IEnumerable<int> themeIds)
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
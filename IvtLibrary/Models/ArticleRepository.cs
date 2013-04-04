using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Web.Security;

namespace IvtLibrary.Models
{ 
    public class ArticleRepository : IArticleRepository
    {
        private readonly IvtLibraryEntities db;

        public ArticleRepository(IvtLibraryEntities db)
        {
            this.db = db;
        }

        public IQueryable<Article> All
        {
            get { return db.Article; }
        }

        public IQueryable<Article> AllIncluding(params Expression<Func<Article, object>>[] includeProperties)
        {
            IQueryable<Article> query = db.Article;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Article Find(int id)
        {
            return db.Article.Single(x => x.id == id);
        }

        public void InsertOrUpdate(Article article)
        {
            if (article.id == default(int)) {
                // New entity
                db.Article.AddObject(article);
            } else {
                // Existing entity
                db.Article.Attach(article);
                db.ObjectStateManager.ChangeObjectState(article, EntityState.Modified);
            }
        }

        public void Delete(int id)
        {
            var article = db.Article.Single(x => x.id == id);
            db.Article.DeleteObject(article);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
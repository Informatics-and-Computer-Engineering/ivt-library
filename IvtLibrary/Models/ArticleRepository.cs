using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using IvtLibrary;

namespace IvtLibrary.Models
{ 
    public class ArticleRepository : IArticleRepository
    {
        IvtLibraryEntities context = new IvtLibraryEntities();

        public IQueryable<Article> All
        {
            get { return context.Article; }
        }

        public IQueryable<Article> AllIncluding(params Expression<Func<Article, object>>[] includeProperties)
        {
            IQueryable<Article> query = context.Article;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Article Find(int id)
        {
            return context.Article.Single(x => x.id == id);
        }

        public void InsertOrUpdate(Article article)
        {
            if (article.id == default(int)) {
                // New entity
                context.Article.AddObject(article);
            } else {
                // Existing entity
                context.Article.Attach(article);
                context.ObjectStateManager.ChangeObjectState(article, EntityState.Modified);
            }
        }

        public void Delete(int id)
        {
            var article = context.Article.Single(x => x.id == id);
            context.Article.DeleteObject(article);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
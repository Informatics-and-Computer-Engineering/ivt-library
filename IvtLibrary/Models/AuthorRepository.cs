using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using IvtLibrary;

namespace IvtLibrary.Models
{ 
    public class AuthorRepository : IAuthorRepository
    {
        IvtLibraryEntities context = new IvtLibraryEntities();

        public IQueryable<Author> All
        {
            get { return context.Author; }
        }

        public IQueryable<Author> AllIncluding(params Expression<Func<Author, object>>[] includeProperties)
        {
            IQueryable<Author> query = context.Author;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Author Find(int id)
        {
            return context.Author.Single(x => x.id == id);
        }

        public void InsertOrUpdate(Author author)
        {
            if (author.id == default(int)) {
                // New entity
                context.Author.AddObject(author);
            } else {
                // Existing entity
                context.Author.Attach(author);
                context.ObjectStateManager.ChangeObjectState(author, EntityState.Modified);
            }
        }

        public void Delete(int id)
        {
            var author = context.Author.Single(x => x.id == id);
            context.Author.DeleteObject(author);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        // заполняет список чекбоксов авторов
        public List<SelectListItem> FillAuthorsCheckBoxList(IEnumerable<Author> authors)
        {
            // получаем список тем, привязанных к автору, если он есть
            HashSet<int> authorIds;
            if (authors != null)
            {
                authorIds = new HashSet<int>(authors.Select(c => c.id));
            }
            else
            {
                authorIds = new HashSet<int>();
            }
            var allAuthors = context.Author;
            var authorsCheckBoxList = new List<SelectListItem>();
            foreach (var author in allAuthors)
            {
                authorsCheckBoxList.Add(new SelectListItem
                {
                    Value = author.id.ToString(),
                    Text = author.first_name + " " + author.middle_name + " " + author.last_name,
                    Selected = authorIds.Contains(author.id)
                });
            }
            return authorsCheckBoxList;
        }
    }
}
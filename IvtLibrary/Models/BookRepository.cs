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
    public class BookRepository : IBookRepository
    {
        IvtLibraryEntities context = new IvtLibraryEntities();

        public IQueryable<Book> All
        {
            get { return context.Book; }
        }

        public IQueryable<Book> AllIncluding(params Expression<Func<Book, object>>[] includeProperties)
        {
            IQueryable<Book> query = context.Book;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Book Find(int id)
        {
            return context.Book.Single(x => x.id == id);
        }

        public void InsertOrUpdate(Book book)
        {
            if (book.id == default(int)) {
                // New entity
                context.Book.AddObject(book);
            } else {
                // Existing entity
                context.Book.Attach(book);
                context.ObjectStateManager.ChangeObjectState(book, EntityState.Modified);
            }
        }

        public void Delete(int id)
        {
            var book = context.Book.Single(x => x.id == id);
            context.Book.DeleteObject(book);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IBookRepository
    {
        IQueryable<Book> All { get; }
        IQueryable<Book> AllIncluding(params Expression<Func<Book, object>>[] includeProperties);
        Book Find(int id);
        void InsertOrUpdate(Book book);
        void Delete(int id);
        void Save();
    }
}
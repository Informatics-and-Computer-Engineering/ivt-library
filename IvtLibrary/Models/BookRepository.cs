using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace IvtLibrary.Models
{ 
    public class BookRepository : IBookRepository
    {
        private readonly IvtLibraryEntities db;

        public BookRepository(IvtLibraryEntities db)
        {
            this.db = db;
        }

        public IQueryable<Book> All
        {
            get { return db.Book; }
        }

        public IQueryable<Book> AllIncluding(params Expression<Func<Book, object>>[] includeProperties)
        {
            IQueryable<Book> query = db.Book;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Book Find(int id)
        {
            return db.Book.Single(x => x.id == id);
        }

        public void InsertOrUpdate(Book book)
        {
            if (book.id == default(int)) {
                // New entity
                db.Book.AddObject(book);
            } else {
                // Existing entity
                db.Book.Attach(book);
                db.ObjectStateManager.ChangeObjectState(book, EntityState.Modified);
            }
        }

        public void Delete(int id)
        {
            var book = db.Book.Single(x => x.id == id);
            db.Book.DeleteObject(book);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
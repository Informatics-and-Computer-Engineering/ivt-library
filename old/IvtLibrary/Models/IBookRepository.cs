using System;
using System.Linq;
using System.Linq.Expressions;

namespace IvtLibrary.Models
{
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
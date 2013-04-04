using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace IvtLibrary.Models
{
    public interface IAuthorRepository
    {
        IQueryable<Author> All { get; }
        IQueryable<Author> AllIncluding(params Expression<Func<Author, object>>[] includeProperties);
        Author Find(int id);
        void InsertOrUpdate(Author author);
        void Delete(int id);
        void Save();
    }
}
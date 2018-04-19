using System;
using System.Linq;
using System.Linq.Expressions;

namespace IvtLibrary.Models
{
    public interface IFileRepository
    {
        IQueryable<File> All { get; }
        IQueryable<File> AllIncluding(params Expression<Func<File, object>>[] includeProperties);
        File Find(int id);
        void InsertOrUpdate(File file);
        void Delete(int id);
        void Save();
    }
}
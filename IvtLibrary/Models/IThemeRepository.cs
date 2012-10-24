using System;
using System.Linq;
using System.Linq.Expressions;

namespace IvtLibrary.Models
{
    public interface IThemeRepository
    {
        IQueryable<Theme> All { get; }
        IQueryable<Theme> AllIncluding(params Expression<Func<Theme, object>>[] includeProperties);
        Theme Find(int id);
        void InsertOrUpdate(Theme theme);
        void Delete(int id);
        void Save();
    }
}
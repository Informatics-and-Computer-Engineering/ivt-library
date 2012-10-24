using System;
using System.Linq;
using System.Linq.Expressions;

namespace IvtLibrary.Models
{
    public interface IResearchRepository
    {
        IQueryable<Research> All { get; }
        IQueryable<Research> AllIncluding(params Expression<Func<Research, object>>[] includeProperties);
        Research Find(int id);
        void InsertOrUpdate(Research research);
        void Delete(int id);
        void Save();
    }
}
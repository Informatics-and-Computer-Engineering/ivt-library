using System;
using System.Linq;
using System.Linq.Expressions;

namespace IvtLibrary.Models
{
    public interface IDraftRepository
    {
        IQueryable<Draft> All { get; }
        IQueryable<Draft> AllIncluding(params Expression<Func<Draft, object>>[] includeProperties);
        Draft Find(long id);
        void InsertOrUpdate(Draft draft);
        void Delete(long id);
        void Save();
    }
}
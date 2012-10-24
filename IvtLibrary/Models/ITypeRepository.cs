using System;
using System.Linq;
using System.Linq.Expressions;

namespace IvtLibrary.Models
{
    public interface ITypeRepository
    {
        IQueryable<Type> All { get; }
        IQueryable<Type> AllIncluding(params Expression<Func<Type, object>>[] includeProperties);
        Type Find(int id);
        void InsertOrUpdate(Type type);
        void Delete(int id);
        void Save();
    }
}
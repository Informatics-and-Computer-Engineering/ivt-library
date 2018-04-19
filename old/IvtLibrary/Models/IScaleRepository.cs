using System;
using System.Linq;
using System.Linq.Expressions;

namespace IvtLibrary.Models
{
    public interface IScaleRepository
    {
        IQueryable<Scale> All { get; }
        IQueryable<Scale> AllIncluding(params Expression<Func<Scale, object>>[] includeProperties);
        Scale Find(int id);
        void InsertOrUpdate(Scale scale);
        void Delete(int id);
        void Save();
    }
}
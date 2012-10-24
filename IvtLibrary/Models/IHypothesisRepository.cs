using System;
using System.Linq;
using System.Linq.Expressions;

namespace IvtLibrary.Models
{
    public interface IHypothesisRepository
    {
        IQueryable<Hypothesis> All { get; }
        IQueryable<Hypothesis> AllIncluding(params Expression<Func<Hypothesis, object>>[] includeProperties);
        Hypothesis Find(long id);
        void InsertOrUpdate(Hypothesis hypothesis);
        void Delete(long id);
        void Save();
    }
}
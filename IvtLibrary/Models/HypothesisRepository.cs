using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using IvtLibrary;

namespace IvtLibrary.Models
{ 
    public class HypothesisRepository : IHypothesisRepository
    {
        IvtLibraryEntities context = new IvtLibraryEntities();

        public IQueryable<Hypothesis> All
        {
            get { return context.Hypothesis; }
        }

        public IQueryable<Hypothesis> AllIncluding(params Expression<Func<Hypothesis, object>>[] includeProperties)
        {
            IQueryable<Hypothesis> query = context.Hypothesis;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Hypothesis Find(long id)
        {
            return context.Hypothesis.Single(x => x.id == id);
        }

        public void InsertOrUpdate(Hypothesis hypothesis)
        {
            if (hypothesis.id == default(long)) {
                // New entity
                context.Hypothesis.AddObject(hypothesis);
            } else {
                // Existing entity
                context.Hypothesis.Attach(hypothesis);
                context.ObjectStateManager.ChangeObjectState(hypothesis, EntityState.Modified);
            }
        }

        public void Delete(long id)
        {
            var hypothesis = context.Hypothesis.Single(x => x.id == id);
            context.Hypothesis.DeleteObject(hypothesis);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

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
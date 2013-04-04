using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Web.Security;

namespace IvtLibrary.Models
{ 
    public class HypothesisRepository : IHypothesisRepository
    {
        private readonly IvtLibraryEntities db;

        public HypothesisRepository(IvtLibraryEntities db)
        {
            this.db = db;
        }

        public IQueryable<Hypothesis> All
        {
            get { return db.Hypothesis; }
        }

        public IQueryable<Hypothesis> AllIncluding(params Expression<Func<Hypothesis, object>>[] includeProperties)
        {
            IQueryable<Hypothesis> query = db.Hypothesis;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Hypothesis Find(long id)
        {
            return db.Hypothesis.Single(x => x.id == id);
        }

        public void InsertOrUpdate(Hypothesis hypothesis)
        {
            if (hypothesis.id == default(long)) {
                // New entity
                db.Hypothesis.AddObject(hypothesis);
            } else {
                // Existing entity
                db.Hypothesis.Attach(hypothesis);
                db.ObjectStateManager.ChangeObjectState(hypothesis, EntityState.Modified);
            }
        }

        public void Delete(long id)
        {
            var hypothesis = db.Hypothesis.Single(x => x.id == id);
            db.Hypothesis.DeleteObject(hypothesis);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
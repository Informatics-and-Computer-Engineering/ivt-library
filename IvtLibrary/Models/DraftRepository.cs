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
    public class DraftRepository : IDraftRepository
    {
        private readonly IvtLibraryEntities db;

        public DraftRepository(IvtLibraryEntities db)
        {
            this.db = db;
        }

        public IQueryable<Draft> All
        {
            get { return db.Draft; }
        }

        public IQueryable<Draft> AllIncluding(params Expression<Func<Draft, object>>[] includeProperties)
        {
            IQueryable<Draft> query = db.Draft;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Draft Find(long id)
        {
            return db.Draft.Single(x => x.id == id);
        }

        public void InsertOrUpdate(Draft draft)
        {
            if (draft.id == default(long)) {
                // New entity
                db.Draft.AddObject(draft);
            } else {
                // Existing entity
                db.Draft.Attach(draft);
                db.ObjectStateManager.ChangeObjectState(draft, EntityState.Modified);
            }
        }

        public void Delete(long id)
        {
            var draft = db.Draft.Single(x => x.id == id);
            db.Draft.DeleteObject(draft);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
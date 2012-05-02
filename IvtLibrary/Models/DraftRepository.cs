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
    public class DraftRepository : IDraftRepository
    {
        IvtLibraryEntities context = new IvtLibraryEntities();

        public IQueryable<Draft> All
        {
            get { return context.Draft; }
        }

        public IQueryable<Draft> AllIncluding(params Expression<Func<Draft, object>>[] includeProperties)
        {
            IQueryable<Draft> query = context.Draft;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Draft Find(long id)
        {
            return context.Draft.Single(x => x.id == id);
        }

        public void InsertOrUpdate(Draft draft)
        {
            if (draft.id == default(long)) {
                // New entity
                context.Draft.AddObject(draft);
            } else {
                // Existing entity
                context.Draft.Attach(draft);
                context.ObjectStateManager.ChangeObjectState(draft, EntityState.Modified);
            }
        }

        public void Delete(long id)
        {
            var draft = context.Draft.Single(x => x.id == id);
            context.Draft.DeleteObject(draft);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

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
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
    public class ScaleRepository : IScaleRepository
    {
        IvtLibraryEntities context = new IvtLibraryEntities();

        public IQueryable<Scale> All
        {
            get { return context.Scale; }
        }

        public IQueryable<Scale> AllIncluding(params Expression<Func<Scale, object>>[] includeProperties)
        {
            IQueryable<Scale> query = context.Scale;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Scale Find(int id)
        {
            return context.Scale.Single(x => x.id == id);
        }

        public void InsertOrUpdate(Scale scale)
        {
            if (scale.id == default(int)) {
                // New entity
                context.Scale.AddObject(scale);
            } else {
                // Existing entity
                context.Scale.Attach(scale);
                context.ObjectStateManager.ChangeObjectState(scale, EntityState.Modified);
            }
        }

        public void Delete(int id)
        {
            var scale = context.Scale.Single(x => x.id == id);
            context.Scale.DeleteObject(scale);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

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
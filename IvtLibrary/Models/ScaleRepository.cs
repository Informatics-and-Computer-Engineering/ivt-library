using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace IvtLibrary.Models
{ 
    public class ScaleRepository : IScaleRepository
    {
        private readonly IvtLibraryEntities db;

        public ScaleRepository(IvtLibraryEntities db)
        {
            this.db = db;
        }

        public IQueryable<Scale> All
        {
            get { return db.Scale; }
        }

        public IQueryable<Scale> AllIncluding(params Expression<Func<Scale, object>>[] includeProperties)
        {
            IQueryable<Scale> query = db.Scale;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Scale Find(int id)
        {
            return db.Scale.Single(x => x.id == id);
        }

        public void InsertOrUpdate(Scale scale)
        {
            if (scale.id == default(int)) {
                // New entity
                db.Scale.AddObject(scale);
            } else {
                // Existing entity
                db.Scale.Attach(scale);
                db.ObjectStateManager.ChangeObjectState(scale, EntityState.Modified);
            }
        }

        public void Delete(int id)
        {
            var scale = db.Scale.Single(x => x.id == id);
            db.Scale.DeleteObject(scale);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
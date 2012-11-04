using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace IvtLibrary.Models
{ 
    public class TypeRepository : ITypeRepository
    {
        private readonly IvtLibraryEntities db;

        public TypeRepository(IvtLibraryEntities db)
        {
            this.db = db;
        }

        public IQueryable<Type> All
        {
            get { return db.Type; }
        }

        public IQueryable<Type> AllIncluding(params Expression<Func<Type, object>>[] includeProperties)
        {
            IQueryable<Type> query = db.Type;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Type Find(int id)
        {
            return db.Type.Single(x => x.id == id);
        }

        public void InsertOrUpdate(Type type)
        {
            if (type.id == default(int)) {
                // New entity
                db.Type.AddObject(type);
            } else {
                // Existing entity
                db.Type.Attach(type);
                db.ObjectStateManager.ChangeObjectState(type, EntityState.Modified);
            }
        }

        public void Delete(int id)
        {
            var type = db.Type.Single(x => x.id == id);
            db.Type.DeleteObject(type);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
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
    public class TypeRepository : ITypeRepository
    {
        IvtLibraryEntities context = new IvtLibraryEntities();

        public IQueryable<Type> All
        {
            get { return context.Type; }
        }

        public IQueryable<Type> AllIncluding(params Expression<Func<Type, object>>[] includeProperties)
        {
            IQueryable<Type> query = context.Type;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Type Find(int id)
        {
            return context.Type.Single(x => x.id == id);
        }

        public void InsertOrUpdate(Type type)
        {
            if (type.id == default(int)) {
                // New entity
                context.Type.AddObject(type);
            } else {
                // Existing entity
                context.Type.Attach(type);
                context.ObjectStateManager.ChangeObjectState(type, EntityState.Modified);
            }
        }

        public void Delete(int id)
        {
            var type = context.Type.Single(x => x.id == id);
            context.Type.DeleteObject(type);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
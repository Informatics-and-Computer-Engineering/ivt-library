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
    public class FileRepository : IFileRepository
    {
        IvtLibraryEntities context = new IvtLibraryEntities();

        public IQueryable<File> All
        {
            get { return context.File; }
        }

        public IQueryable<File> AllIncluding(params Expression<Func<File, object>>[] includeProperties)
        {
            IQueryable<File> query = context.File;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public File Find(int id)
        {
            return context.File.Single(x => x.id == id);
        }

        public void InsertOrUpdate(File file)
        {
            if (file.id == default(int)) {
                // New entity
                context.File.AddObject(file);
            } else {
                // Existing entity
                context.File.Attach(file);
                context.ObjectStateManager.ChangeObjectState(file, EntityState.Modified);
            }
        }

        public void Delete(int id)
        {
            var file = context.File.Single(x => x.id == id);
            context.File.DeleteObject(file);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
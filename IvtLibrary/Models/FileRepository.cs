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
    public class FileRepository : IFileRepository
    {
        private readonly IvtLibraryEntities db;

        public FileRepository(IvtLibraryEntities db)
        {
            this.db = db;
        }

        public IQueryable<File> All
        {
            get { return db.File; }
        }

        public IQueryable<File> AllIncluding(params Expression<Func<File, object>>[] includeProperties)
        {
            IQueryable<File> query = db.File;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public File Find(int id)
        {
            return db.File.Single(x => x.id == id);
        }

        public void InsertOrUpdate(File file)
        {
            if (file.id == default(int)) {
                // New entity
                db.File.AddObject(file);
            } else {
                // Existing entity
                db.File.Attach(file);
                db.ObjectStateManager.ChangeObjectState(file, EntityState.Modified);
            }
        }

        public void Delete(int id)
        {
            var file = db.File.Single(x => x.id == id);
            db.File.DeleteObject(file);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
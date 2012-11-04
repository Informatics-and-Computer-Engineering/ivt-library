using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace IvtLibrary.Models
{ 
    public class ResearchRepository : IResearchRepository
    {
        private readonly IvtLibraryEntities db;

        public ResearchRepository(IvtLibraryEntities db)
        {
            this.db = db;
        }

        public IQueryable<Research> All
        {
            get { return db.Research; }
        }

        public IQueryable<Research> AllIncluding(params Expression<Func<Research, object>>[] includeProperties)
        {
            IQueryable<Research> query = db.Research;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Research Find(int id)
        {
            return db.Research.Single(x => x.id == id);
        }

        public void InsertOrUpdate(Research research)
        {
            if (research.id == default(int)) {
                // New entity
                db.Research.AddObject(research);
            } else {
                // Existing entity
                db.Research.Attach(research);
                db.ObjectStateManager.ChangeObjectState(research, EntityState.Modified);
            }
        }

        public void Delete(int id)
        {
            var research = db.Research.Single(x => x.id == id);
            db.Research.DeleteObject(research);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
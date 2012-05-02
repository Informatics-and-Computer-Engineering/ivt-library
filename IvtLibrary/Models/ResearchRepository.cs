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
    public class ResearchRepository : IResearchRepository
    {
        IvtLibraryEntities context = new IvtLibraryEntities();

        public IQueryable<Research> All
        {
            get { return context.Research; }
        }

        public IQueryable<Research> AllIncluding(params Expression<Func<Research, object>>[] includeProperties)
        {
            IQueryable<Research> query = context.Research;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Research Find(int id)
        {
            return context.Research.Single(x => x.id == id);
        }

        public void InsertOrUpdate(Research research)
        {
            if (research.id == default(int)) {
                // New entity
                context.Research.AddObject(research);
            } else {
                // Existing entity
                context.Research.Attach(research);
                context.ObjectStateManager.ChangeObjectState(research, EntityState.Modified);
            }
        }

        public void Delete(int id)
        {
            var research = context.Research.Single(x => x.id == id);
            context.Research.DeleteObject(research);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IResearchRepository
    {
        IQueryable<Research> All { get; }
        IQueryable<Research> AllIncluding(params Expression<Func<Research, object>>[] includeProperties);
        Research Find(int id);
        void InsertOrUpdate(Research research);
        void Delete(int id);
        void Save();
    }
}
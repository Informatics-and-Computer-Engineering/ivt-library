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
    public class ConferenceRepository : IConferenceRepository
    {
        IvtLibraryEntities context = new IvtLibraryEntities();

        public IQueryable<Conference> All
        {
            get { return context.Conference; }
        }

        public IQueryable<Conference> AllIncluding(params Expression<Func<Conference, object>>[] includeProperties)
        {
            IQueryable<Conference> query = context.Conference;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Conference Find(int id)
        {
            return context.Conference.Single(x => x.id == id);
        }

        public void InsertOrUpdate(Conference conference)
        {
            if (conference.id == default(int)) {
                // New entity
                context.Conference.AddObject(conference);
            } else {
                // Existing entity
                context.Conference.Attach(conference);
                context.ObjectStateManager.ChangeObjectState(conference, EntityState.Modified);
            }
        }

        public void Delete(int id)
        {
            var conference = context.Conference.Single(x => x.id == id);
            context.Conference.DeleteObject(conference);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
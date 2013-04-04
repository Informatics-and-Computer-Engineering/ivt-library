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
    public class ConferenceRepository : IConferenceRepository
    {
        private readonly IvtLibraryEntities db;

        public ConferenceRepository(IvtLibraryEntities db)
        {
            this.db = db;
        }

        public IQueryable<Conference> All
        {
            get { return db.Conference; }
        }

        public IQueryable<Conference> AllIncluding(params Expression<Func<Conference, object>>[] includeProperties)
        {
            IQueryable<Conference> query = db.Conference;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Conference Find(int id)
        {
            return db.Conference.Single(x => x.id == id);
        }

        public void InsertOrUpdate(Conference conference)
        {
            if (conference.id == default(int)) {
                // New entity
                db.Conference.AddObject(conference);
            } else {
                // Existing entity
                db.Conference.Attach(conference);
                db.ObjectStateManager.ChangeObjectState(conference, EntityState.Modified);
            }
        }

        public void Delete(int id)
        {
            var conference = db.Conference.Single(x => x.id == id);
            db.Conference.DeleteObject(conference);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
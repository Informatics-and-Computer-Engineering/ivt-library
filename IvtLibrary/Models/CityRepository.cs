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
    public class CityRepository : ICityRepository
    {
        private readonly IvtLibraryEntities db;

        public CityRepository(IvtLibraryEntities db)
        {
            this.db = db;
        }

        public IQueryable<City> All
        {
            get { return db.City; }
        }

        public IQueryable<City> AllIncluding(params Expression<Func<City, object>>[] includeProperties)
        {
            IQueryable<City> query = db.City;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public City Find(int id)
        {
            return db.City.Single(x => x.id == id);
        }

        public void InsertOrUpdate(City city)
        {
            if (city.id == default(int)) {
                // New entity
                db.City.AddObject(city);
            } else {
                // Existing entity
                db.City.Attach(city);
                db.ObjectStateManager.ChangeObjectState(city, EntityState.Modified);
            }
        }

        public void Delete(int id)
        {
            var city = db.City.Single(x => x.id == id);
            db.City.DeleteObject(city);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
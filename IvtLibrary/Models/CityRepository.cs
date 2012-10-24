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
    public class CityRepository : ICityRepository
    {
        IvtLibraryEntities context = new IvtLibraryEntities();

        public IQueryable<City> All
        {
            get { return context.City; }
        }

        public IQueryable<City> AllIncluding(params Expression<Func<City, object>>[] includeProperties)
        {
            IQueryable<City> query = context.City;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public City Find(int id)
        {
            return context.City.Single(x => x.id == id);
        }

        public void InsertOrUpdate(City city)
        {
            if (city.id == default(int)) {
                // New entity
                context.City.AddObject(city);
            } else {
                // Existing entity
                context.City.Attach(city);
                context.ObjectStateManager.ChangeObjectState(city, EntityState.Modified);
            }
        }

        public void Delete(int id)
        {
            var city = context.City.Single(x => x.id == id);
            context.City.DeleteObject(city);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
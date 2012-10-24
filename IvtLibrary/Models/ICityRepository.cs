using System;
using System.Linq;
using System.Linq.Expressions;

namespace IvtLibrary.Models
{
    public interface ICityRepository
    {
        IQueryable<City> All { get; }
        IQueryable<City> AllIncluding(params Expression<Func<City, object>>[] includeProperties);
        City Find(int id);
        void InsertOrUpdate(City city);
        void Delete(int id);
        void Save();
    }
}
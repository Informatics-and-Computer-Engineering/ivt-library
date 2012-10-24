using System;
using System.Linq;
using System.Linq.Expressions;

namespace IvtLibrary.Models
{
    public interface IConferenceRepository
    {
        IQueryable<Conference> All { get; }
        IQueryable<Conference> AllIncluding(params Expression<Func<Conference, object>>[] includeProperties);
        Conference Find(int id);
        void InsertOrUpdate(Conference conference);
        void Delete(int id);
        void Save();
    }
}
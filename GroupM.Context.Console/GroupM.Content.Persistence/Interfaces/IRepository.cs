using GroupM.Content.Entities;
using System.Collections.Generic;

namespace GroupM.Content.Persistence.Interfaces
{
    public interface IRepository<T> 
        where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void SaveChanges();
    }
}

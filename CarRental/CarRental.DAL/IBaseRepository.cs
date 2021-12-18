using System;
using System.Linq;

namespace CarRental.DAL.Interfaces
{
    public interface IBaseRepository<TEntity> : IDisposable
        where TEntity : class
    {
        // return Created Entity
        void Create(TEntity item);
        // IEnumerable
        IQueryable<TEntity> GetAll();
        TEntity Get(Guid id);
        // Return updated entity
        void Update(TEntity item);
        void Remove(Guid id);
        // Unnecessary ?
        void Save();
    }
}

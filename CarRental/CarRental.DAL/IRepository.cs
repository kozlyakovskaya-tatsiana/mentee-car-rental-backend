using System;
using System.Linq;

namespace CarRental.DAL
{
    public interface IGenericRepository<TEntity> : IDisposable
        where TEntity : class
    {
        void Create(TEntity item);
        IQueryable<TEntity> GetAll();
        TEntity Get(Guid id);
        void Update(TEntity item);
        void Remove(Guid id);
        void Save();
    }
}

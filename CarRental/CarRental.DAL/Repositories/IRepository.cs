using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.DAL.Repositories
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        IEnumerable<TEntity> GetEntityList();
        void Create(TEntity item);
        IQueryable<TEntity> GetAll();
        TEntity Get(Guid id);
        void Update(TEntity item);
        void Remove(Guid id);
        void Save();
    }
}

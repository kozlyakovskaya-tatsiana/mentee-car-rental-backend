using System;
using System.Collections.Generic;

namespace CarRental.DAL
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        TEntity Create(TEntity item);
        IEnumerable<TEntity> GetAll();
        TEntity Get(Guid id);
        TEntity Update(TEntity item);
        void Remove(Guid id);
    }
}

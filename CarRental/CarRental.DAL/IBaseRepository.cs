using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.DAL
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        // List IEnumerable IList Iqeryable
        Task<List<TEntity>> GetAll();
        Task<TEntity> Get(Guid id);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(Guid id);
    }
}

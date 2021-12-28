using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.DAL
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAll();
        Task<TEntity> Get(Guid id);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(TEntity entity);
    }
}
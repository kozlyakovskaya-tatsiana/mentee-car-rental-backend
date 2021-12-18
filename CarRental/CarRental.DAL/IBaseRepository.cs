using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.DAL
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> Get(int id);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(int id);
    }
}

using System;
using CarRental.DAL.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.DAL
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        private readonly CarRentalDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(CarRentalDbContext context)
        {
            this._context = context;
            this._dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            _dbSet.Add(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Delete(TEntity entity)
        {
            _dbSet.Remove(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Get(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public Task<IQueryable<TEntity>> GetAll()
        {
            return Task.FromResult(_dbSet.AsQueryable());
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _dbSet.Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
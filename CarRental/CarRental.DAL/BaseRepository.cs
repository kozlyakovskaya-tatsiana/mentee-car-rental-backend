using System;
using System.Collections.Generic;
using CarRental.DAL.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.DAL
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        protected readonly CarRentalDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        protected BaseRepository(CarRentalDbContext context)
        {
            this.Context = context;
            this.DbSet = Context.Set<TEntity>();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            DbSet.Add(entity);

            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Delete(TEntity entity)
        {
            DbSet.Remove(entity);

            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Get(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IQueryable<TEntity>> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            DbSet.Update(entity);

            await Context.SaveChangesAsync();

            return entity;
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories.Implementation
{
    public class RefreshTokenRepository : BaseRepository<RefreshTokenEntity>, IRefreshTokenRepository
    {

        public RefreshTokenRepository(CarRentalDbContext context) : base(context)
        { }

        public Task<RefreshTokenEntity> Get(Guid userId, string token)
        {
            return DbSet.FirstOrDefaultAsync(u => u.UserId == userId && u.Token == token);
        }

        public async Task Revoke(Guid id)
        {
            var collection = DbSet.AsQueryable().Where(x => x.UserId == id);
            foreach (var entity in collection)
            {
                DbSet.Remove(entity);
            }

            await Context.SaveChangesAsync();
        }
    }
}
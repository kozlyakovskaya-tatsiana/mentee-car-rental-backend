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
        private readonly DbSet<RefreshTokenEntity> _dbSet;
        private readonly CarRentalDbContext _context;

        public RefreshTokenRepository(CarRentalDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.RefreshTokens;
        }

        public Task<RefreshTokenEntity> Get(Guid userId, string token)
        {
            return _dbSet.FirstOrDefaultAsync(u => u.UserId == userId && u.Token == token);
        }

        public async Task Revoke(Guid id)
        {
            var collection = _dbSet.AsQueryable().Where(x => x.UserId == id);
            foreach (var entity in collection)
            {
                _dbSet.Remove(entity);
            }

            await _context.SaveChangesAsync();
        }
    }
}
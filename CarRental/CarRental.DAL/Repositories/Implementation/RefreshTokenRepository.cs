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
        public RefreshTokenRepository(CarRentalDbContext context) : base(context)
        {
            _dbSet = context.RefreshTokens;
        }

        public Task<RefreshTokenEntity> Get(string token)
        {
            return _dbSet.FirstOrDefaultAsync(u => u.Token == token);
        }

        public void Revoke(Guid id)
        {
            _dbSet.Remove(_dbSet.FindAsync(id).Result);
        }
    }
}

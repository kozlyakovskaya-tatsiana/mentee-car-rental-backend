using System.Linq;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories.Implementation
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        private readonly DbSet<UserEntity> _dbSet;

        public UserRepository(CarRentalDbContext context) : base(context)
        {
            _dbSet = context.Users;
        }

        public UserEntity Get(string email)
        {
            return _dbSet.FirstOrDefault(u => u.Email == email);
        }
    }
}
using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories.Implementation
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {

        public UserRepository(CarRentalDbContext context) : base(context)
        { }

        public async Task<UserEntity> Get(string email)
        {
            return await DbSet.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
using System.Linq;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories.Implementation
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {

        public UserRepository(CarRentalDbContext context) : base(context)
        { }

        public UserEntity Get(string email)
        {
            return DbSet.FirstOrDefault(u => u.Email == email);
        }
    }
}
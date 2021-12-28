using System.Threading.Tasks;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        UserEntity Get(string email);
    }
}
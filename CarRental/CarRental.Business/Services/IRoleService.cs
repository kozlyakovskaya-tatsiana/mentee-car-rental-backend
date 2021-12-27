using System.Threading.Tasks;
using CarRental.Business.Models.Role;
using CarRental.DAL.Entities;

namespace CarRental.Business.Services
{
    public interface IRoleService
    {
        public Task<RoleEntity> Create(RoleCreateModel model);
        public Task<UserEntity> UpdateUserRoles(UserRoleModel model);
    }
}

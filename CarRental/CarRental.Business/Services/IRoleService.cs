using System.Threading.Tasks;
using CarRental.Business.Models.Role;

namespace CarRental.Business.Services
{
    public interface IRoleService
    {
        public Task CreateRole(RoleCreateModel model);
        public Task UpdateUserRoles(UserRoleModel model);
    }
}

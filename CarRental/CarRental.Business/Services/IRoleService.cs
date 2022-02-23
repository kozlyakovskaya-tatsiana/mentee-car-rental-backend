using System.Threading.Tasks;
using CarRental.Business.Models.Role;

namespace CarRental.Business.Services
{
    public interface IRoleService
    {
        public Task CreateRole(CreateRoleModel model);
        public Task UpdateUserRoles(UserRoleModel model);
    }
}

using System.Threading.Tasks;
using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.Business.Identity.Policy;
using CarRental.Business.Models.Role;
using CarRental.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        private readonly IMapper _mapper;

        public RoleController(
            IMapper mapper,
            IRoleService roleService
        )
        {
            _mapper = mapper;
            _roleService = roleService;
        }

        [Authorize(Policy = Policy.AdminPolicy)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateRole(CreateRoleRequest request)
        {
            var model = _mapper.Map<CreateRoleRequest, CreateRoleModel>(request);
            await _roleService.CreateRole(model);

            return Ok();
        }

        [Authorize(Policy = Policy.AdminPolicy)]
        [HttpPost("user/update")]
        public async Task<IActionResult> AddUserToRole([FromBody]AddRoleRequest request)
        {
            var model = _mapper.Map<AddRoleRequest, UserRoleModel>(request);
            await _roleService.UpdateUserRoles(model);

            return Ok();
        }
    }
}
using System;
using System.Threading.Tasks;
using CarRental.Business.Models.User;
using CarRental.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/management/user/{id}")]
    [ApiController]
    public class UserManagementController : Controller
    {
        private readonly IUserService _userService;

        public UserManagementController(
            IUserService userService
            )
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> ShowInfo(Guid id)
        {
            var result = await _userService.GetUserInfo(id);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveUser(Guid id)
        {
            var result = await _userService.RemoveUser(id);

            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateExistingUser(Guid id, [FromBody] UserInfoModel model)
        {
            var result = await _userService.ModifyUser(id, model);

            return Ok();
        }
    }
}

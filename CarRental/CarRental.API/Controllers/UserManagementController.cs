﻿using System;
using System.Threading.Tasks;
using CarRental.Business.Models.User;
using CarRental.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/management/users/")]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> ShowUserInfo(Guid id)
        {
            var result = await _userService.GetUserInfo(id);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveUser(Guid id)
        {
            var result = await _userService.RemoveUser(id);

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateExistingUser(Guid id, [FromBody] UserInfoModel model)
        {
            var result = await _userService.ModifyUser(id, model);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();

            return Ok(result);
        }
    }
}
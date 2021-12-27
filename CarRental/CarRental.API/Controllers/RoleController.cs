﻿using System.Threading.Tasks;
using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.Business.Models.Role;
using CarRental.Business.Services;
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


        [HttpPost("all")]
        public async Task<IActionResult> CreateRole(RoleCreateRequest request)
        {
            var model = _mapper.Map<RoleCreateRequest, RoleCreateModel>(request);
            await _roleService.Create(model);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> AddUserToRole([FromBody]AddRoleRequest request)
        {
            var model = _mapper.Map<AddRoleRequest, UserRoleModel>(request);
            await _roleService.UpdateUserRoles(model);
            return Ok();
        }
    }
}
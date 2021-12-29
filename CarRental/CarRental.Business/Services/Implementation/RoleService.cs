using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Business.Models.Role;
using CarRental.Common.Exceptions;
using CarRental.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Business.Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly UserManager<UserEntity> _userManager;

        private readonly IMapper _mapper;

        public RoleService(
            RoleManager<RoleEntity> roleManager, 
            IMapper mapper, 
            UserManager<UserEntity> userManager
            )
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task CreateRole(RoleCreateModel role)
        {
            var newRole = _mapper.Map<RoleCreateModel, RoleEntity>(role);
            var roleResult = await _roleManager.CreateAsync(newRole);
            if (!roleResult.Succeeded)
            {
                throw new Exception(roleResult.Errors.First().Description);
            }
        }

        public async Task UpdateUserRoles(UserRoleModel model)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == model.UserEmail);
            if (user == null)
            {
                throw new NotFoundException("User with this username doesn't exist.");
            }
            var result = await _userManager.AddToRoleAsync(user, model.RoleName);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
        }
    }
}
﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Business.Models.Role;
using CarRental.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Business.Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;

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

        public async Task<RoleEntity> Create(RoleCreateModel role)
        {
            var newRole = _mapper.Map<RoleCreateModel, RoleEntity>(role);
            var roleResult = await _roleManager.CreateAsync(newRole);
            if (!roleResult.Succeeded)
            {
                throw new Exception(roleResult.Errors.First().Description);
            }

            return newRole;
        }

        public async Task<UserEntity> UpdateUserRoles(UserRoleModel model)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == model.UserEmail);
            if (user == null)
            {
                //User doesn't exist
                throw new Exception();
            }
            var result = await _userManager.AddToRoleAsync(user, model.RoleName);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            return user;
        }
    }
}
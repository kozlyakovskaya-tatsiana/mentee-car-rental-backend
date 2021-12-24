using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Business.Models.User;
using CarRental.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Business.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly IMapper _mapper;

        public UserService(
            IMapper mapper,
            UserManager<UserEntity> userManager,
            RoleManager<RoleEntity> roleManager
        )
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        async Task<IdentityResult> IUserService.CreateAsync(RegisterModel model)
        {
            var user = _mapper.Map<RegisterModel, UserEntity>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }

        
    }
}
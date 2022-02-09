using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Common.Options;
using AutoMapper;
using CarRental.Business.Models.User;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CarRental.Business.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;

        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;

        private readonly JwtOptions _jwtOptions;

        private readonly IMapper _mapper;

        public UserService(
            UserManager<UserEntity> userManager,
            IRefreshTokenRepository refreshTokenRepository,
            IOptions<JwtOptions> jwtOptions,
            IMapper mapper,
            IUserRepository userRepository
        )
        {
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<string> AttachNewRefreshTokenToUser(Guid userId, string refresh)
        {
            var refreshEntity = new RefreshTokenEntity
            {
                Token = refresh,
                UserId = userId,
                Expired = DateTime.UtcNow.AddMinutes(_jwtOptions.RefreshTokenDurationInMinutes)
            };

            await _refreshTokenRepository.Add(refreshEntity);

            return refresh;
        }

        public async Task<UserInfoModel> GetUserInfo(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var result = _mapper.Map<UserEntity, UserInfoModel>(user);

            return result;
        }

        public async Task<UserInfoModel> RemoveUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var result = _mapper.Map<UserEntity, UserInfoModel>(user);

            await _userManager.DeleteAsync(user);

            return result;
        }

        public async Task<UserInfoModel> ModifyUser(Guid id, UserInfoModel model)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            foreach (var prop in typeof(UserInfoModel).GetProperties())
            {
                var userProp = typeof(UserEntity).GetProperty(prop.Name);
                var value = prop.GetValue(model);
                if (value != null && userProp != null)
                {
                    userProp.SetValue(user, value);
                }
            }

            user.NormalizedEmail = user.Email.ToUpper();

            await _userManager.UpdateAsync(user);

            var result = _mapper.Map<UserEntity, UserInfoModel>(user);
            return result;
        }

        public async Task<List<UserInfoModel>> GetAllUsers()
        {
            var users = (await _userRepository.GetAll()).ToArray();
            var result = new List<UserInfoModel>();

            foreach (var user in users)
            {
                var infoModel = _mapper.Map<UserEntity, UserInfoModel>(user);
                infoModel.Roles = (List<string>) await _userManager.GetRolesAsync(user);
                result.Add(infoModel);
            }

            return result;
        }
    }
}
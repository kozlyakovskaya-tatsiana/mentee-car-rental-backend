using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRental.Business.Models.User;
using CarRental.DAL.Entities;

namespace CarRental.Business.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<Claim>> GenerateUserClaims(UserEntity user);
        public Task<String> AttachNewRefreshTokenToUser(Guid userId, string refresh);
        public Task<UserInfoModel> GetUserInfo(Guid id);
        public Task<UserInfoModel> RemoveUser(Guid id);
        public Task<UserInfoModel> ModifyUser(Guid id, UserInfoModel model);
    }
}
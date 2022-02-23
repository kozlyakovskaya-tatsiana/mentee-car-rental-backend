using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Business.Models.User;

namespace CarRental.Business.Services
{
    public interface IUserService
    {
        public Task<string> AttachNewRefreshTokenToUser(Guid userId, string refresh);
        public Task<UserInfoModel> GetUserInfo(Guid id);
        public Task<UserInfoModel> RemoveUser(Guid id);
        public Task<UserInfoModel> ModifyUser(Guid id, UserInfoModel model);
        public Task<List<UserInfoModel>> GetAllUsers();
    }
}
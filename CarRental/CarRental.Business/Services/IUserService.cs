using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRental.Business.Models.Token;
using CarRental.Business.Models.User;
using CarRental.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Business.Services
{
    public interface IUserService
    {
        public Task<IdentityResult> Register(RegisterModel model);
        public Task<TokenPairModel> Login(LoginModel model);
        public Task<TokenPairModel> CreateTokenPair(UserEntity user);
        public Task<IEnumerable<Claim>> GenerateUserClaims(UserEntity user);
        public Task<UserEntity> IsUserExist(LoginModel model);
        public Task<RefreshTokenEntity> AttachNewRefreshToUser(UserEntity user, string refresh);
    }
}
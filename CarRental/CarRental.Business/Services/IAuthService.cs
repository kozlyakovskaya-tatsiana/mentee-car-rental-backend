using System.Threading.Tasks;
using CarRental.Business.Models.Token;
using CarRental.Business.Models.User;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Business.Services
{
    public interface IAuthService
    {
        public Task<IdentityResult> Register(RegisterModel model);
        public Task<TokenPairModel> Login(LoginModel model);
        public TokenPairModel RefreshTokenPair(TokenPairModel model);
        public UserIdModel VerifyAccessToken(TokenPairModel model);
    }
}

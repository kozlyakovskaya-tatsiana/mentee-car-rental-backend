using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRental.Business.Models;
using CarRental.Business.Models.User;
using CarRental.DAL.Entities;

namespace CarRental.Business.Services
{
    public interface ITokenService
    {
        public RefreshTokenEntity GenerateRefresh(UserEntity user);
        public JwtSecurityToken GenerateJwt(IEnumerable<Claim> claims);
        public bool IsRefreshExpired(RefreshTokenEntity pair);
        public Task<TokenPairModel> GenerateTokenPair(LoginModel user);
        public TokenPairModel GenerateTokenPair(TokenPairModel pair);
        public IEnumerable<Claim> CreateUserClaims(UserEntity user);
        public TokenPairModel Revoke(TokenPairModel pair);
    }
}

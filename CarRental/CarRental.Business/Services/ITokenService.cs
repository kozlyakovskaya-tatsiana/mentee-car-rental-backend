using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRental.Business.Models;
using CarRental.Business.Models.Token;
using CarRental.Business.Models.User;
using CarRental.DAL.Entities;

namespace CarRental.Business.Services
{
    public interface ITokenService
    {
        /*
        public RefreshTokenEntity GenerateRefresh(UserEntity user);
        public JwtSecurityToken GenerateJwt(IEnumerable<Claim> claims);
        public bool IsRefreshExpired(RefreshTokenEntity pair);
        public Task<TokenPairModel> GenerateTokenPair(LoginModel user);
        public TokenPairModel UpdateTokenPair(TokenPairModel pair);
        public IEnumerable<Claim> CreateUserClaims(UserEntity user);
        public TokenRevokeModel Revoke(TokenRevokeModel pair);
        */
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}

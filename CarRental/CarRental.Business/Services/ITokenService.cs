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
        public TokenPairModel UpdateAccessToken(TokenPairModel model);
        public TokenRevokeModel Revoke(TokenRevokeModel model);
        public bool IsRefreshExpired(RefreshTokenEntity refresh);
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}

using System.Collections.Generic;
using System.Security.Claims;
using CarRental.Business.Models.Token;
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
        ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}
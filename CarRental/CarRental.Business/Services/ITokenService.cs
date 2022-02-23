using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarRental.Business.Services
{
    public interface ITokenService
    {
        public TaskStatus Revoke(Guid id);
        public string GenerateAccessToken(IEnumerable<Claim> claims);
        public string GenerateRefreshToken();
        public ClaimsPrincipal GetPrincipalFromToken(string token);
        public bool IsRefreshTokenExpired(DateTimeOffset expiredTime);
    }
}
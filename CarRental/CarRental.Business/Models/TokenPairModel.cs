using System.IdentityModel.Tokens.Jwt;
using CarRental.DAL.Entities;

namespace CarRental.Business.Models
{
    public class TokenPairModel
    {
        public JwtSecurityToken AccessToken { get; set; }
        public RefreshTokenEntity RefreshToken { get; set; }
    }
}

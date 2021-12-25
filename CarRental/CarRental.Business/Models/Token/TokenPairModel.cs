using System.IdentityModel.Tokens.Jwt;
using CarRental.DAL.Entities;

namespace CarRental.Business.Models.Token
{
    public class TokenPairModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CarRental.Business.Models.Token;
using CarRental.Business.Options;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.Business.Services.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public TokenService(
            IOptions<JwtOptions> jwt,
            IRefreshTokenRepository refreshTokenRepository
        )
        {
            _refreshTokenRepository = refreshTokenRepository;
            _jwtOptions = jwt.Value;
        }

        public TokenPairModel UpdateAccessToken(TokenPairModel model)
        {
            var identity = GetPrincipalFromToken(model.AccessToken);
            var idClaim = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            var userId = Guid.Parse(idClaim.Value);
            var refresh = _refreshTokenRepository.Get(userId, model.RefreshToken);
            if (refresh == null || IsRefreshExpired(refresh.Result))
            {
                // Your refresh token expired, re-login pls
                throw new Exception();
            }
            var newAccessToken = GenerateAccessToken(identity.Claims);
            return new TokenPairModel
            {
                AccessToken = newAccessToken,
                RefreshToken = refresh.Result.Token
            };
        }

        public TokenRevokeModel Revoke(TokenRevokeModel model)
        {
            var identity = GetPrincipalFromToken(model.AccessToken);
            var idClaim = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (idClaim == null)
            {
                //Refresh tokens doesn't exist
                throw new Exception();
            }

            var id = Guid.Parse(idClaim.Value);

            _refreshTokenRepository.Revoke(id);

            return model;
        }
        
        public bool IsRefreshExpired(RefreshTokenEntity refresh)
        {
            return refresh.Expired < DateTime.UtcNow;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.DurationInMinutes),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = _jwtOptions.ValidateAudience,
                ValidAudience = _jwtOptions.Audience,
                ValidateIssuer = _jwtOptions.ValidateIssuer,
                ValidIssuer = _jwtOptions.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)),
                ValidateLifetime = true
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}
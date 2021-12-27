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
        private readonly JwtOptions _jwt;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public TokenService(
            IOptions<JwtOptions> jwt,
            IRefreshTokenRepository refreshTokenRepository
        )
        {
            _refreshTokenRepository = refreshTokenRepository;
            _jwt = jwt.Value;
        }


        public TokenPairModel UpdateAccessToken(TokenPairModel model)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(model.AccessToken);
            var claims = token.Claims;
            var userId = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var refresh = _refreshTokenRepository.Get(Guid.Parse(userId), model.RefreshToken);
            if (refresh == null || IsRefreshExpired(refresh.Result))
            {
                // You refresh token expired, re-login pls
                throw new Exception();
            }
            else
            {
                var newAccessToken = GenerateAccessToken(claims);
                model.AccessToken = newAccessToken;
                return model;
            }
        }


        public TokenRevokeModel Revoke(TokenRevokeModel model)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(model.AccessToken);
            var id = token.Claims.FirstOrDefault(
                         x => x.Type == ClaimTypes.NameIdentifier)?.Value
                     ?? throw new InvalidOperationException();
            _refreshTokenRepository.Revoke(Guid.Parse(id));
            return model;
        }


        public bool IsRefreshExpired(RefreshTokenEntity refresh)
        {
            return refresh.Expired < DateTime.UtcNow;
        }


        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
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

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)),
                ValidateLifetime = true
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}
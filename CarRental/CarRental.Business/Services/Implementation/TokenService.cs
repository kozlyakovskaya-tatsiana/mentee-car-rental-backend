using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Business.Models;
using CarRental.Business.Models.Token;
using CarRental.Business.Models.User;
using CarRental.Business.Options;
using CarRental.DAL;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;
using CarRental.DAL.Repositories.Implementation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.Business.Services.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwt;


        public TokenService(
            IMapper mapper,
            IOptions<JwtOptions> jwt
        )
        {
            _jwt = jwt.Value;
        }

        /*
        public TokenPairModel UpdateTokenPair(TokenPairModel pair)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(pair.AccessToken);
            var claims = token.Claims;
            var userId = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var refresh = _refreshTokenRepository.Get(Guid.Parse(userId), pair.RefreshToken);
            if (refresh == null || IsRefreshExpired(refresh.Result))
            {
                // You refresh token expired, re-login pls
                throw new Exception();
            }
            else
            {
                var newJwt = GenerateJwt(claims);
                pair.AccessToken = new JwtSecurityTokenHandler().WriteToken(newJwt);
                return pair;
            }
        }

        public IEnumerable<Claim> CreateUserClaims(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, (user.FirstName + user.LastName)),
            };

            return claims;
        }

        public TokenRevokeModel Revoke(TokenRevokeModel pair)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(pair.AccessToken);
            var id = token.Claims.FirstOrDefault(
                x => x.Type == ClaimTypes.NameIdentifier)?.Value 
                     ?? throw new InvalidOperationException();
            _refreshTokenRepository.Revoke(Guid.Parse(id));
            return pair;
        }

        public JwtSecurityToken GenerateJwt(IEnumerable<Claim> claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials
                );
            return jwtSecurityToken;
        }


        public bool IsRefreshExpired(RefreshTokenEntity refresh)
        {
            return refresh.Expired < DateTime.UtcNow;
        }

        public async Task<TokenPairModel> GenerateTokenPair(LoginModel model)
        {
            var user = _userRepository.Get(model.Email);
            var claims = CreateUserClaims(user);
            var jwt = GenerateJwt(claims);
            var refresh = GenerateRefresh(user);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            await _refreshTokenRepository.Add(refresh);
            var result = new TokenPairModel {AccessToken = encodedJwt, RefreshToken = refresh.Token};
            return result;
        }

        public RefreshTokenEntity GenerateRefresh(UserEntity user)
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);
            var refreshSecurityToken = new RefreshTokenEntity
            {
                Token = Convert.ToBase64String(randomNumber),
                Expired = DateTime.UtcNow.AddMinutes(_jwt.RefreshTokenDurationInMinutes),
                User = user,
                UserId = user.Id
            };
            return refreshSecurityToken;
        }
        */
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
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
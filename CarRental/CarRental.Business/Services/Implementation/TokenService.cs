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
        private readonly IMapper _mapper;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly JwtOptions _jwt;
        private readonly IUserRepository _userRepository;


        public TokenService(
            IMapper mapper,
            IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository,
            IOptions<JwtOptions> jwt
        )
        {
            _mapper = mapper;
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _jwt = jwt.Value;
        }

        public TokenPairModel GenerateTokenPair(TokenPairModel pair)
        {
            var userId = pair.AccessToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var refresh = _refreshTokenRepository.Get(Guid.Parse(userId)).Result;

            if (IsRefreshExpired(refresh))
            {
                // You refresh token expired, re-login pls
                throw new Exception();
            }
            else
            {
                //TODO
                return null;
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

        public TokenPairModel Revoke(TokenPairModel pair)
        {
            // Refresh tokens doesn't exist
            var id = Guid.Parse(
                pair.AccessToken.Claims.FirstOrDefault(
                    x => x.Type == ClaimTypes.NameIdentifier)?.Value
                ?? throw new InvalidOperationException());
            _refreshTokenRepository.Revoke(id);
            return pair;
        }

        // Complete
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


        // 
        public bool IsRefreshExpired(RefreshTokenEntity refresh)
        {
            var tokens = _refreshTokenRepository.Get(refresh.Token);
            return tokens.Result.Expired > DateTime.Now;
        }

        // Complete
        public async Task<TokenPairModel> GenerateTokenPair(LoginModel model)
        {
            var user = _userRepository.Get(model.Email);
            var claims = CreateUserClaims(user);
            var jwt = GenerateJwt(claims);
            var refresh = GenerateRefresh(user);
            // PROBLEM: Doesn't add to database
            await _refreshTokenRepository.Add(refresh);
            var result = new TokenPairModel {AccessToken = jwt, RefreshToken = refresh};
            return result;
        }

        // Complete
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
    }
}
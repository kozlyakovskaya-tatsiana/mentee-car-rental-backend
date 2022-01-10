using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRental.Common.Options;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CarRental.Business.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;

        private readonly IRefreshTokenRepository _refreshTokenRepository;

        private readonly JwtOptions _jwtOptions;

        public UserService(
            UserManager<UserEntity> userManager,
            IRefreshTokenRepository refreshTokenRepository,
            IOptions<JwtOptions> jwtOptions
        )
        {
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<IEnumerable<Claim>> GenerateUserClaims(UserEntity user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim("roles", role)).ToList();

            var claims = new List<Claim>
            {

                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };
            var result = claims.Union(roleClaims);

            return result;
        }

        public async Task<String> AttachNewRefreshTokenToUser(Guid userId, string refresh)
        {
            var refreshEntity = new RefreshTokenEntity
            {
                Token = refresh,
                UserId = userId,
                Expired = DateTime.UtcNow.AddMinutes(_jwtOptions.RefreshTokenDurationInMinutes)
            };

            await _refreshTokenRepository.Add(refreshEntity);

            return refresh;
        }
    }
}
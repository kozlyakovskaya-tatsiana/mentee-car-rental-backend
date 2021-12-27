using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Business.Models.Token;
using CarRental.Business.Models.User;
using CarRental.Business.Options;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CarRental.Business.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<RoleEntity> _roleManager;

        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        private readonly IMapper _mapper;

        private readonly JwtOptions _jwtOptions;

        public UserService(
            IMapper mapper,
            UserManager<UserEntity> userManager,
            RoleManager<RoleEntity> roleManager,
            ITokenService tokenService,
            IRefreshTokenRepository refreshTokenRepository,
            IOptions<JwtOptions> jwtOptions
        )
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<IdentityResult> Register(RegisterModel model)
        {
            var user = _mapper.Map<RegisterModel, UserEntity>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, _roleManager.FindByNameAsync("user").Result.ToString());
            }

            return result;
        }

        public async Task<TokenPairModel> Login(LoginModel model)
        {
            var user = await IsUserExist(model);
            var tokenPair = await CreateTokenPair(user);
            return tokenPair;
        }

        public async Task<TokenPairModel> CreateTokenPair(UserEntity user)
        {
            var claims = await GenerateUserClaims(user);
            var access = _tokenService.GenerateAccessToken(claims);
            var refresh = _tokenService.GenerateRefreshToken();
            await AttachNewRefreshToUser(user, refresh);
            var result = new TokenPairModel
            {
                RefreshToken = refresh,
                AccessToken = access,
            };

            return result;
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

        public async Task<UserEntity> IsUserExist(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // User doesn't exist
                throw new Exception();
            }
            var verify = _userManager.CheckPasswordAsync(user, model.Password);
            if (!verify.Result)
            {
                // Wrong password
                // return Unauthorized();
                throw new Exception();
            }

            return user;
        }

        public async Task<RefreshTokenEntity> AttachNewRefreshToUser(UserEntity user, string refresh)
        {
            var refreshEntity = new RefreshTokenEntity
            {
                Token = refresh,
                User = user,
                UserId = user.Id,
                Expired = DateTime.UtcNow.AddMinutes(_jwtOptions.RefreshTokenDurationInMinutes)
            };

            await _refreshTokenRepository.Add(refreshEntity);
            return refreshEntity;
        }
    }
}
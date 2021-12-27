using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

        private readonly IMapper _mapper;

        private readonly IRefreshTokenRepository _refreshTokenRepository;

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

        async Task<IdentityResult> IUserService.Register(RegisterModel model)
        {
            var user = _mapper.Map<RegisterModel, UserEntity>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }

        public async Task<TokenPairModel> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // User doesn't exist
                // return Unauthorized();
                throw new Exception();
            }

            var verifyPassword =
                _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
            if (verifyPassword == PasswordVerificationResult.Failed)
            {
                // Wrong password
                // return Unauthorized();
                throw new Exception();
            }

            var tokenPairModel = await CreateTokenPair(user);
            return tokenPairModel;
        }

        public async Task<TokenPairModel> CreateTokenPair(UserEntity user)
        {
            var claims = GenerateUserClaims(user);
            var access = _tokenService.GenerateAccessToken(claims);
            var refresh = _tokenService.GenerateRefreshToken();
            var refreshEntity = new RefreshTokenEntity
            {
                Token = refresh,
                User = user,
                UserId = user.Id,
                Expired = DateTime.UtcNow.AddMinutes(_jwtOptions.RefreshTokenDurationInMinutes)
            };
            await _refreshTokenRepository.Add(refreshEntity);
            var result = new TokenPairModel
            {
                RefreshToken = refresh,
                AccessToken = access,
            };
            return result;
        }

        public List<Claim> GenerateUserClaims(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };
            return claims;
        }
    }
}
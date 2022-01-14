using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Business.Identity.Role;
using CarRental.Business.Models.Token;
using CarRental.Business.Models.User;
using CarRental.Common.Exceptions;
using CarRental.Common.Options;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.Business.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<RoleEntity> _roleManager;

        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        private readonly IRefreshTokenRepository _refreshTokenRepository;

        private readonly IMapper _mapper;

        private readonly JwtOptions _jwtOptions;

        public AuthService(
            IMapper mapper,
            UserManager<UserEntity> userManager,
            RoleManager<RoleEntity> roleManager,
            ITokenService tokenService, 
            IUserService userService, 
            IOptions<JwtOptions> jwtOptions, 
            IRefreshTokenRepository refreshTokenRepository
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _userService = userService;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<IdentityResult> Register(RegisterModel model)
        {
            var user = _mapper.Map<RegisterModel, UserEntity>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, _roleManager.FindByNameAsync(Role.UserRole).Result.ToString());
            }

            return result;
        }

        public async Task<TokenPairModel> Login(LoginModel model)
        {
            var user = await IsUserAuthenticate(model);
            var tokenPair = await CreateTokenPair(user);

            return tokenPair;
        }

        public TokenPairModel RefreshTokenPair(TokenPairModel model)
        {
            var identity = _tokenService.GetPrincipalFromToken(model.AccessToken);
            var idClaim = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var userId = Guid.Parse(idClaim.Value);

            var refresh = _refreshTokenRepository.Get(userId, model.RefreshToken);
            if (refresh == null || _tokenService.IsRefreshTokenExpired(refresh.Result.Expired))
            {
                throw new TokenExpiredException("Refresh token expired.");
            }

            var newAccessToken = _tokenService.GenerateAccessToken(identity.Claims);

            refresh.Result.Token = _tokenService.GenerateRefreshToken();
            refresh.Result.Expired = DateTime.UtcNow.AddMinutes(_jwtOptions.RefreshTokenDurationInMinutes);

            _refreshTokenRepository.Update(refresh.Result);

            return new TokenPairModel
            {
                AccessToken = newAccessToken,
                RefreshToken = refresh.Result.Token
            };
        }

        public UserIdModel VerifyAccessToken(TokenPairModel model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var key = Encoding.ASCII.GetBytes(_jwtOptions.Key);
                tokenHandler.ValidateToken(model.AccessToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = _jwtOptions.ValidateIssuer,
                    ValidateAudience = _jwtOptions.ValidateAudience,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken) validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var result = new UserIdModel()
                {
                    Id = Guid.Parse(userId)
                };

                return result;
            }
            catch
            {
                throw new NotVerifiedException("Access Token invalid");
            }
        }

        private async Task<UserEntity> IsUserAuthenticate(LoginModel model)
        {
            var user = await IsUserExist(model.Email);

            var verify = _userManager.CheckPasswordAsync(user, model.Password);
            if (!verify.Result)
            {
                throw new BadAuthorizeException("Wrong password entered.");
            }

            return user;
        }

        private async Task<UserEntity> IsUserExist(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new NotFoundException("User with this email doesn't exist.");
            }
            
            return user;
        }

        private async Task<TokenPairModel> CreateTokenPair(UserEntity user)
        {
            var claims = await _userService.GenerateUserClaims(user);
            var access = _tokenService.GenerateAccessToken(claims);
            var refresh = _tokenService.GenerateRefreshToken();
            await _userService.AttachNewRefreshTokenToUser(user.Id, refresh);

            return new TokenPairModel
            {
                RefreshToken = refresh,
                AccessToken = access,
            };
        }
    }
}

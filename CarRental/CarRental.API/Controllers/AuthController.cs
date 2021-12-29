using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.Business.Models.Token;
using CarRental.Business.Models.User;
using CarRental.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;
        public AuthController(
            IAuthService authService,
            IMapper mapper,
            ITokenService tokenService, 
            ILogger<AuthController> logger
            )
        {
            _authService = authService;
            _mapper = mapper;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("token/revoke")]
        public IActionResult RevokeAllTokens(Guid userId)
        {
            _tokenService.Revoke(userId);
            _logger.LogInformation("User with id: {Id} revoke all refresh tokens", userId);
            return Ok();
        }

        [HttpPost("token/refresh")]
        public IActionResult RefreshTokenPair(GetTokenPairRequest pair)
        {
            var request = _mapper.Map<GetTokenPairRequest, TokenPairModel>(pair);
            var result = _authService.RefreshTokenPair(request);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> SignUp(RegisterRequest userSignUpRequest)
        {
            _logger.LogInformation("User with email: {Email} try to register.", userSignUpRequest.Email);
            var user = _mapper.Map<RegisterRequest, RegisterModel>(userSignUpRequest);
            var result = await _authService.Register(user);
            if (result.Succeeded)
            {
                _logger.LogInformation("User with email: {Email} register successful.", userSignUpRequest.Email);
            }
            else
            {
                _logger.LogInformation("User with email: {Email} not registered with error: {Error}.", userSignUpRequest.Email, result.Errors.First().Description);
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn(LoginRequest userLoginRequest)
        {
            _logger.LogInformation("User with email: {Email} try to login.", userLoginRequest.Email);
            var user = _mapper.Map<LoginRequest, LoginModel>(userLoginRequest);
            var result = await _authService.Login(user);
            _logger.LogInformation("User with email: {Email} login successful.", userLoginRequest.Email);

            return Ok(result);
        }
    }
}

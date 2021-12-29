using System;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.Business.Models.Token;
using CarRental.Business.Models.User;
using CarRental.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        private readonly IMapper _mapper;

        public AuthController(
            IAuthService authService,
            IMapper mapper,
            ITokenService tokenService
        )
        {
            _authService = authService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("token/revoke")]
        public IActionResult RevokeAllTokens(Guid userId)
        {
            _tokenService.Revoke(userId);

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
            var user = _mapper.Map<RegisterRequest, RegisterModel>(userSignUpRequest);
            await _authService.Register(user);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn(LoginRequest userLoginRequest)
        {
            var user = _mapper.Map<LoginRequest, LoginModel>(userLoginRequest);
            var result = await _authService.Login(user);

            return Ok(result);
        }
    }
}

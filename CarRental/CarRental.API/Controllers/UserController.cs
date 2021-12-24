using System.Threading.Tasks;
using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.API.Models.Responses;
using CarRental.Business.Models;
using CarRental.Business.Models.User;
using CarRental.Business.Options;
using CarRental.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CarRental.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserController(
            IMapper mapper,
            IUserService userService, 
            ITokenService tokenService,
            IOptions<JwtOptions> jwt)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> SignUp(RegisterRequest userSignUpRequest)
        {
            var user = _mapper.Map<RegisterRequest, RegisterModel>(userSignUpRequest);
            var result = await _userService.CreateAsync(user);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn(LoginRequest userLoginRequest)
        {
            var user = _mapper.Map<LoginRequest, LoginModel>(userLoginRequest);
            var tokenPair = await _tokenService.GenerateTokenPair(user);
            var result = _mapper.Map<TokenPairModel, TokenPairResponse>(tokenPair);
            return Ok(result);
        }
    }
}

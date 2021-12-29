using System.Threading.Tasks;
using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.Business.Models.User;
using CarRental.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;

        private readonly IMapper _mapper;

        public UserController(
            IMapper mapper,
            IAuthService authService
        )
        {
            _authService = authService;
            _mapper = mapper;
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
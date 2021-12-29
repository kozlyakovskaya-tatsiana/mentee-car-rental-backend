using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.API.Models.Responses;
using CarRental.API.Validators;
using CarRental.Business.Models.Token;
using CarRental.Business.Models.User;
using CarRental.Business.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;

        private readonly IMapper _mapper;

        private readonly RegisterValidator _registerValidator;
        public UserController(
            IMapper mapper,
            IAuthService authService,
            RegisterValidator registerValidator
        )
        {
            _registerValidator = registerValidator;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> SignUp(RegisterRequest userSignUpRequest)
        {
            // Where to place the method? 
            var validationResult = await _registerValidator.ValidateAsync(userSignUpRequest);
            List<string> ValidationMessages = new List<string>();

            var response = new ValidationResponse();
            if (!validationResult.IsValid)
            {
                response.IsValid = false;
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    ValidationMessages.Add(failure.ErrorMessage);
                }
                response.ValidationMessages = ValidationMessages;
            }

            var user = _mapper.Map<RegisterRequest, RegisterModel>(userSignUpRequest);
            await _authService.Register(user);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn(LoginRequest userLoginRequest)
        {
            var user = _mapper.Map<LoginRequest, LoginModel>(userLoginRequest);
            var tokenPair = await _authService.Login(user);
            var result = _mapper.Map<TokenPairModel, TokenPairResponse>(tokenPair);
            return Ok(result);
        }
    }
}
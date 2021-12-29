using System;
using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.Business.Models.Token;
using CarRental.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        private readonly IMapper _mapper;

        public TokenController(
            IAuthService authService,
            IMapper mapper,
            ITokenService tokenService
            )
        {
            _authService = authService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("revoke")]
        public IActionResult RevokeAllTokens(Guid id)
        {
            _tokenService.Revoke(id);
            return Ok();
        }

        [HttpPost("update")]
        public IActionResult UpdateAccessToken(TokenPairRequest pair)
        {
            var request = _mapper.Map<TokenPairRequest, TokenPairModel>(pair);
            var result = _authService.RefreshTokenPair(request);
            return Ok(result);
        }
    }
}
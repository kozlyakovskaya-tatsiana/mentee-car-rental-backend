using System.Threading.Tasks;
using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.API.Models.Responses;
using CarRental.Business.Models;
using CarRental.Business.Models.Token;
using CarRental.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public TokenController(
            ITokenService tokenService, 
            IMapper mapper
            )
        {
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeAllTokens(TokenPairRequest pair)
        {
            var request = _mapper.Map<TokenPairRequest, TokenRevokeModel>(pair);
            _tokenService.Revoke(request);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAccessToken(TokenPairRequest pair)
        {
            var request = _mapper.Map<TokenPairRequest, TokenPairModel>(pair);
            var newTokens = _tokenService.UpdateTokenPair(request);
            var result = _mapper.Map<TokenPairModel, TokenPairResponse>(newTokens);
            return Ok(result);
        }
    }
}

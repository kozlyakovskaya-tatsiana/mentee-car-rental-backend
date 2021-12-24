using System.Threading.Tasks;
using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.Business.Models;
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
            var request = _mapper.Map<TokenPairRequest, TokenPairModel>(pair);
            _tokenService.Revoke(request);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update()
        {
            return Ok();
        }
    }
}

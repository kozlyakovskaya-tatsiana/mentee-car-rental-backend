using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace carRental.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/secured")]
    [ApiController]
    public class SecuredController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetSecuredData()
        {
            return Ok("Hello world");
        }
    }
}

using CarRental.Business.Identity.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace carRental.API.Controllers
{
    
    [Route("api/secured")]
    [ApiController]
    public class SecuredController : ControllerBase
    {
        [Authorize]
        [HttpGet("auth")]
        public IActionResult GetSecuredData()
        {
            return Ok("Hello world");
        }

        [Authorize (Policy = Policy.AdminPolicy)]
        [HttpGet("Superadmin")]
        public IActionResult getSuperAdminData()
        {
            return Ok("Hello world");
        }
    }
}
using CarRental.Business.Identity.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace carRental.API.Controllers
{
    
    [Route("api/secured")]
    [ApiController]
    public class SecuredController : ControllerBase
    {
        [HttpGet("auth"), Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetSecuredData()
        {
            return Ok("Hello world");
        }

        [HttpGet("Superadmin"), Authorize(AuthenticationSchemes = "Bearer", Policy = Policy.AdminPolicy)]
        public IActionResult getSuperAdminData()
        {
            return Ok("Hello world");
        }
    }
}
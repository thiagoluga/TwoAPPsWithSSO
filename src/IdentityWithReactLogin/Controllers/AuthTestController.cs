using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWithReactLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthTestController : ControllerBase
    {
        [HttpGet]
        [Route("no-auth")]
        [AllowAnonymous]
        public async Task<IActionResult> TestMethodWithoutAuthentication()
        {
            return Ok("No auth is required here");
        }

        [HttpGet]
        [Route("with-auth")]
        [Authorize]
        public async Task<IActionResult> TestMethodWithAuthentication()
        {
            return Ok("Authentication is required here. If you receive this message, then you are authenticated!!");
        }
    }
}

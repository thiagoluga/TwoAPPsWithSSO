using IdentityWithReactLogin.Services;
using IdentityWithReactLogin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWithReactLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountsController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await this.accountService.FindByEmailAsync(registerViewModel.Email);
            if (user != null)
            {
                return Conflict("Email already exist");
            }

            var result = await this.accountService.CreateAsync(registerViewModel);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(this.CreateAsync), result.Succeeded);
            }

            return BadRequest("Error");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await this.accountService.LoginAsync(loginViewModel);

            if (result.Succeeded)
            {
                var fullJwt = this.accountService.GetUserResponse(loginViewModel.Email);
                return Ok(fullJwt);
            }

            if (result.IsLockedOut)
            {
                return Unauthorized("This user is temporarily blocked");
            }

            return Unauthorized("Incorrect user or password");
        }
    }
}

using Identity.Configurations;
using Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NetDevPack.Identity.Jwt;
using NetDevPack.Identity.Jwt.Model;

namespace Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly AppJwtSettings jwtSettings;

        public AccountService(
             UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signInManager,
             IOptions<AppJwtSettings> jwtSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtSettings = jwtSettings.Value;
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel loginViewModel)
        {
            var result = await signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, false, true);
            return result;
        }

        public async Task<IdentityResult> CreateAsync(RegisterViewModel registerViewModel)
        {
            var user = new IdentityUser
            {
                UserName = registerViewModel.Email,
                Email = registerViewModel.Email
            };

            var result = await userManager.CreateAsync(user, registerViewModel.Password);

            //if (result.Succeeded)
            //{
            //    await AddUpdateUserClaimAsync(ClaimTypes.Name, model.Nome, user);
            //    return new DefaultResponse(model.Email, true);
            //}

            return result;
        }

        public async Task<IdentityUser> FindByEmailAsync(string email)
        {
            var result = await userManager.FindByEmailAsync(email);
            return result;
        }

        public UserResponse<string> GetUserResponse(string email)
        {
            return new JwtBuilder<IdentityUser>()
                .WithUserManager(userManager)
                .WithJwtSettings(jwtSettings)
                .WithEmail(email)
                .WithJwtClaims()
                .WithUserClaims()
                .WithUserRoles()
                .BuildUserResponse();
        }
    }
}

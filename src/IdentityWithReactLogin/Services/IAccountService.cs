using IdentityWithReactLogin.ViewModels;
using Microsoft.AspNetCore.Identity;
using NetDevPack.Identity.Jwt.Model;

namespace IdentityWithReactLogin.Services
{
    public interface IAccountService
    {
        Task<SignInResult> LoginAsync(LoginViewModel loginViewModel);

        Task<IdentityResult> CreateAsync(RegisterViewModel registerViewModel);

        Task<IdentityUser> FindByEmailAsync(string email);

        UserResponse<string> GetUserResponse(string email);
    }
}

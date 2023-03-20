using System.ComponentModel.DataAnnotations;

namespace IdentityWithReactLogin.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string? Password { get; set; }
    }
}

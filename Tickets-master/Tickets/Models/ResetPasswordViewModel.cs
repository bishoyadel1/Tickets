using System.ComponentModel.DataAnnotations;

namespace Tickets.Models
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(7, ErrorMessage = "Min Length is 7 ")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}

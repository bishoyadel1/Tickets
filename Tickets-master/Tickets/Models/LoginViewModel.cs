using System.ComponentModel.DataAnnotations;

namespace Tickets.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage = "Enter vaild email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(7,ErrorMessage = "Min Length is 7 ")]
        public string Password { get; set; }

    }
}

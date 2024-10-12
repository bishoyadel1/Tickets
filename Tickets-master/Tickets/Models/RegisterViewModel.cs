using System.ComponentModel.DataAnnotations;

namespace Tickets.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Enter vaild email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(7, ErrorMessage = "Min Length is 7 ")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(7, ErrorMessage = "Min Length is 7 ")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public bool IsOrganizer { get; set; } = false;


    }
}

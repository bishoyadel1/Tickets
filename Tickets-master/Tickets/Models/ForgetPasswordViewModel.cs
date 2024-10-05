using System.ComponentModel.DataAnnotations;

namespace Tickets.Models
{
    public class ForgetPasswordViewModel
    {

        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage = "Enter vaild email address")]
        public string Email { get; set; }
   

    }
}

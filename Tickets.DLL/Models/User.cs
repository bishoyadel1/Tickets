using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.DLL.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [PasswordPropertyText]
        [Required]
        public string Password { get; set; }
        public DateTime DateOfBrith { get; set; }
        [Required]
        public string? Adderss { get; set; }

        public ICollection<Event> Events { get; set; }
        public Organizer Organizer { get; set; }
    }
}

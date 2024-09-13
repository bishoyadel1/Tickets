using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.DLL.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
      
        public DateTime Date { get; set; }
        [Required]
        public string? Image { get; set; }
        public string  Description { get; set; }
        public bool Approved { get; set; }
        public User User { get; set; }
        public Organizer Organizer { get; set; }
    }
}

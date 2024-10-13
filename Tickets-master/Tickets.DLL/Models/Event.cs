using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tickets.DLL.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
      
        public DateTime Date { get; set; }

        [Required]
        public int TotalNumberOfTickets { get; set; }
        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        [Required]
        public double TicketPrice { get; set; }
        public string  Description { get; set; }
        public string? OrganizerId { get; set; } = null;
        public ICollection<IdentityUser> Users { get; set; }

        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
    }
}

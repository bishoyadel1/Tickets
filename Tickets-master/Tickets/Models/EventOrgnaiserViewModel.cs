using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.DLL.Models
{
    public class EventOrgnaiserViewModel
    {
        public int EventId { get; set; }
        [Required]
        public string EventName { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public int TotalNumberOfTickets { get; set; }
        public string? ImageUrl { get; set; }
        public double Price  { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
        public string Description { get; set; }

        public string OrganizerName { get; set; }

    }
}

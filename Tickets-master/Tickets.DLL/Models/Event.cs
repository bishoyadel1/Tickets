using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Tickets.DLL.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
      
        public DateTime Date { get; set; }

        [Required]
        public int TotalNumberOfTicekts { get; set; }
        public string? Image { get; set; }
        public string  Description { get; set; }
        public string OrganizerId { get; set; }
        public ICollection<IdentityUser> Users { get; set; }

        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
    }
}

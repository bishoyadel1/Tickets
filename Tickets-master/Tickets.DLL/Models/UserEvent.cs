using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.DLL.Models
{
    public class UserEvent
    {
        public int Id { get; set; }
        public Event Event { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
     
        public IdentityUser User { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Tickets.Models
{
    public class EventDetailsViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public DateTime Date { get; set; }
        [Required]
        public int TotalNumberOfTicekts { get; set; }
        public string? Image { get; set; }
        public string Description { get; set; }
        public string OrganizerId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public string OrganizerName { get; set; }






    }
}

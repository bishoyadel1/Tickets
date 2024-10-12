namespace Tickets.Models
{
    public class UsersRoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool  IsAdmin { get; set; }
        public bool IsOrganizer { get; set; }


    }
}

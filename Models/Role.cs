namespace Training_Session_Booking_Portal.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;

        // Navigation: One Role can have many Users
        public ICollection<User> Users { get; set; } = new List<User>();

    }
}

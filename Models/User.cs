namespace Training_Session_Booking_Portal.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public Role? Role { get; set; } // Navigation to Role
        public DateTime CreatedAt { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "India Standard Time");

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Session> SessionsAsTrainer { get; set; } = new List<Session>();

    }
}

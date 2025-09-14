namespace Training_Session_Booking_Portal.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }     // Navigation to User

        public int SessionId { get; set; }
        public Session? Session { get; set; }   // Navigation to Session

        public DateTime RegisteredAt { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "India Standard Time");
        public bool Status { get; set; } = true; // 1 = Registered, 0 = Withdrawn
    }
}

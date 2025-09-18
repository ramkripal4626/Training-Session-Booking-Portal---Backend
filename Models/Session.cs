namespace Training_Session_Booking_Portal.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TrainerId { get; set; }
        public User? Trainer { get; set; }
        public int Capacity { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "India Standard Time");
        public bool IsApproved { get; set; } = false;
        public byte[]? RowVersion { get; set; }

        public string? MeetingLink { get; set; }  

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}

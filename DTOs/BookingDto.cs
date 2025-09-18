namespace Training_Session_Booking_Portal.DTOs
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public int SessionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? TrainerName { get; set; }
        public string? MeetingLink { get; set; }

    }
}

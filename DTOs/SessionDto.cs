namespace Training_Session_Booking_Portal.DTOs
{
    public class SessionDto
    {
        public int SessionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Capacity { get; set; }
        public bool IsApproved { get; set; }
        public string? TrainerName { get; set; }  // flatten trainer info

        public string? MeetingLink { get; set; }  // <-- add this



    }
}

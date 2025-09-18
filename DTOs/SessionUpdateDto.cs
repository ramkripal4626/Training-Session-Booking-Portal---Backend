namespace Training_Session_Booking_Portal.DTOs
{
    public class SessionUpdateDto
    {
        public int SessionId { get; set; } // Required to identify the session
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Capacity { get; set; }
        public bool IsApproved { get; set; } // Admin can approve via PUT
        public string? TrainerName { get; set; } // Optional, for display only

        public string? MeetingLink { get; set; }  // <-- add this


    }
}

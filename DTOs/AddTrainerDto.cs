namespace Training_Session_Booking_Portal.DTOs
{
    public class AddTrainerDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; } 
    }

}

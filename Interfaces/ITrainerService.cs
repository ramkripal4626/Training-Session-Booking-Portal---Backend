using Training_Session_Booking_Portal.DTOs;
using Training_Session_Booking_Portal.Models;

namespace Training_Session_Booking_Portal.Interfaces
{
    public interface ITrainerService
    {
        Task<string> CreateSessionAsync(int trainerId, SessionDto dto);
        Task<List<Session>> GetMySessionsAsync(int trainerId);

    }
}

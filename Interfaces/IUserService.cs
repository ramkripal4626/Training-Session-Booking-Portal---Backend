using Training_Session_Booking_Portal.DTOs;
using Training_Session_Booking_Portal.Models;

namespace Training_Session_Booking_Portal.Interfaces
{
    public interface IUserService
    {
        Task<List<SessionDto>> GetAvailableSessionsAsync();
        Task<SessionDto?> GetSessionByIdAsync(int sessionId);
        Task<string> RegisterSessionAsync(int sessionId, int userId);
        Task<List<BookingDto>> GetMySessionsAsync(int userId);
        Task<string> WithdrawFromSessionAsync(int sessionId, int userId);

    }
}

using System.Security.Claims;
using Training_Session_Booking_Portal.DTOs;
using Training_Session_Booking_Portal.Models;

namespace Training_Session_Booking_Portal.Interfaces
{
    public interface ISessionService
    {
        Task<List<SessionResponseDto>> GetAllSessionsAsync();
        Task<SessionResponseDto> GetSessionByIdAsync(int sessionId);

        // Trainer creates session (not auto-approved)
        Task<string> CreateSessionAsync(Session session, ClaimsPrincipal user);

        Task<string> UpdateSessionAsync(SessionUpdateDto session);
        Task<string> DeleteSessionAsync(int sessionId);

        // Admin action
        Task<string> ApproveSessionAsync(int sessionId, bool approve);

    }

}

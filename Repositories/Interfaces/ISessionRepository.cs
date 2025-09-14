using Training_Session_Booking_Portal.DTOs;
using Training_Session_Booking_Portal.Models;

namespace Training_Session_Booking_Portal.Repositories.Interfaces
{
    public interface ISessionRepository
    {
        Task<List<Session>> GetAllSessionsAsync();

        Task<Session?> GetSessionByIdAsync(int sessionId);
        Task AddSessionAsync(Session session);
        Task UpdateSessionAsync(Session session);
        Task DeleteSessionAsync(Session session);

    }
}

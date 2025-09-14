using Training_Session_Booking_Portal.Models;

namespace Training_Session_Booking_Portal.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<Session>> GetAvailableSessionsAsync();
        Task<bool> IsAlreadyRegisteredAsync(int sessionId, int userId);
        Task<Session?> GetSessionByIdAsync(int sessionId);
        Task<int> GetBookingCountAsync(int sessionId);
        Task AddBookingAsync(Booking booking);
        Task<List<Booking>> GetUserBookingsAsync(int userId);
        Task<Booking?> GetBookingAsync(int sessionId, int userId);
        Task RemoveBookingAsync(Booking booking);

    }
}

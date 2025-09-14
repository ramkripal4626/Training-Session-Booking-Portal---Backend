using Training_Session_Booking_Portal.Models;

namespace Training_Session_Booking_Portal.Interfaces
{
    public interface IUserService
    {
        Task<List<Session>> GetAvailableSessionsAsync();
        Task<string> RegisterSessionAsync(int sessionId, int userId);
        Task<List<Booking>> GetMySessionsAsync(int userId);
        Task<string> WithdrawFromSessionAsync(int sessionId, int userId);

    }
}

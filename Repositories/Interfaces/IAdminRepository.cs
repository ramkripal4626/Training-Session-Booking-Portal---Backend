using Training_Session_Booking_Portal.Models;

namespace Training_Session_Booking_Portal.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Session>> GetPendingSessionsAsync();
        Task<Session?> GetSessionByIdAsync(int id);
        Task ApproveSessionAsync(Session session);
        Task RejectSessionAsync(Session session);

        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);

        Task AddTrainerAsync(User trainer);
        Task UpdateUserAsync(User user);
        Task DeleteTrainerAsync(User trainer);

        Task<int> GetPendingSessionsCountAsync();
        Task<int> GetApprovedSessionsCountAsync();
        Task<int> GetTrainersCountAsync();



    }
}

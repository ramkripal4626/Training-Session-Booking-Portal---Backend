using Training_Session_Booking_Portal.Models;

namespace Training_Session_Booking_Portal.Repositories.Interfaces
{
    public interface ITrainerRepository
    {
        Task<Session> CreateSessionAsync(Session session);
        Task<List<Session>> GetSessionsByTrainerAsync(int trainerId);

    }
}

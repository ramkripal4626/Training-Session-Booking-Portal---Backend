using Training_Session_Booking_Portal.DTOs;
using Training_Session_Booking_Portal.Models;

namespace Training_Session_Booking_Portal.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<Session>> GetPendingSessionsAsync();
        Task<bool> ApproveSessionAsync(int id);
        Task<bool> RejectSessionAsync(int id);

        Task<bool> AddTrainerAsync(AddTrainerDto dto);

        Task<bool> PromoteUserToTrainerAsync(int userId);
        Task<bool> DeleteTrainerAsync(int id);


    }
}

using Training_Session_Booking_Portal.Models;

namespace Training_Session_Booking_Portal.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<Role?> GetRoleByIdAsync(int roleId);
        Task<Role?> GetRoleByNameAsync(string roleName);
        Task<bool> EmailExistsAsync(string email);
        Task<User> AddUserAsync(User user);

    }
}

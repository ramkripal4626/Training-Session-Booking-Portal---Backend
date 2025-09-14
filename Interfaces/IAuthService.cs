using Training_Session_Booking_Portal.DTOs;

namespace Training_Session_Booking_Portal.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterDto dto);
        Task<AuthResponse> LoginAsync(LoginDto dto);

    }
}

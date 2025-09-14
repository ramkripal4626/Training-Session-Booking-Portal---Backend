using Training_Session_Booking_Portal.Interfaces;
using Training_Session_Booking_Portal.Models;
using Training_Session_Booking_Portal.Repositories.Interfaces;

namespace Training_Session_Booking_Portal.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Session>> GetAvailableSessionsAsync()
        {
            return await _repository.GetAvailableSessionsAsync();
        }

        public async Task<string> RegisterSessionAsync(int sessionId, int userId)
        {
            if (await _repository.IsAlreadyRegisteredAsync(sessionId, userId))
                return "You are already registered for this session.";

            var session = await _repository.GetSessionByIdAsync(sessionId);
            if (session == null || !session.IsApproved)
                return "Session not available.";

            var currentBookings = await _repository.GetBookingCountAsync(sessionId);
            if (currentBookings >= session.Capacity)
                return "Session is full.";

            var booking = new Booking
            {
                UserId = userId,
                SessionId = sessionId
            };

            await _repository.AddBookingAsync(booking);
            return "Registered successfully.";
        }

        public async Task<List<Booking>> GetMySessionsAsync(int userId)
        {
            return await _repository.GetUserBookingsAsync(userId);
        }

        public async Task<string> WithdrawFromSessionAsync(int sessionId, int userId)
        {
            var booking = await _repository.GetBookingAsync(sessionId, userId);
            if (booking == null)
                return "Booking not found.";

            await _repository.RemoveBookingAsync(booking);
            return "Withdrawn from session successfully.";
        }
    }

}

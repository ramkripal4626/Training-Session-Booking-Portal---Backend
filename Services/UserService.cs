using Training_Session_Booking_Portal.DTOs;
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

        public async Task<List<SessionDto>> GetAvailableSessionsAsync()
        {
            var sessions = await _repository.GetAvailableSessionsAsync();

            return sessions.Select(s => new SessionDto
            {
                SessionId = s.SessionId,
                Title = s.Title,
                Description = s.Description,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Capacity = s.Capacity,
                IsApproved = s.IsApproved,
                MeetingLink = s.MeetingLink, // ✅ new field
                TrainerName = s.Trainer != null
                    ? $"{s.Trainer.FirstName} {s.Trainer.LastName}"
                    : "Unknown"
            }).ToList();
        }

        public async Task<SessionDto?> GetSessionByIdAsync(int sessionId)
        {
            var s = await _repository.GetSessionByIdAsync(sessionId);
            if (s == null) return null;

            return new SessionDto
            {
                SessionId = s.SessionId,
                Title = s.Title,
                Description = s.Description,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Capacity = s.Capacity,
                IsApproved = s.IsApproved,
                MeetingLink = s.MeetingLink,
                TrainerName = s.Trainer != null
                    ? $"{s.Trainer.FirstName} {s.Trainer.LastName}"
                    : "Unknown"
            };
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



        public async Task<List<BookingDto>> GetMySessionsAsync(int userId)
        {
            var bookings = await _repository.GetUserBookingsAsync(userId);

            return bookings.Select(b => new BookingDto
            {
                BookingId = b.BookingId,
                SessionId = b.SessionId,
                Title = b.Session?.Title ?? string.Empty,
                Description = b.Session?.Description,
                StartTime = b.Session?.StartTime ?? DateTime.MinValue,
                EndTime = b.Session?.EndTime ?? DateTime.MinValue,
                TrainerName = b.Session?.Trainer != null
                ? $"{b.Session.Trainer.FirstName} {b.Session.Trainer.LastName}"
                : null,
                MeetingLink = b.Session?.MeetingLink
            }).ToList();
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

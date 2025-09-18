using System.Security.Claims;
using Training_Session_Booking_Portal.DTOs;
using Training_Session_Booking_Portal.Interfaces;
using Training_Session_Booking_Portal.Models;
using Training_Session_Booking_Portal.Repositories.Interfaces;

namespace Training_Session_Booking_Portal.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _repository;

        public SessionService(ISessionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SessionResponseDto>> GetAllSessionsAsync()
        {
            var sessions = await _repository.GetAllSessionsAsync();

            return sessions.Select(s => new SessionResponseDto
            {
                SessionId = s.SessionId,
                Title = s.Title,
                Description = s.Description,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Capacity = s.Capacity,
                IsApproved = s.IsApproved,
                TrainerId = s.TrainerId,
                TrainerName = s.Trainer != null
                    ? $"{s.Trainer.FirstName} {s.Trainer.LastName}"
                    : "Unknown",
                MeetingLink = s.MeetingLink
            }).ToList();
        }

        public async Task<SessionResponseDto?> GetSessionByIdAsync(int sessionId)
        {
            var session = await _repository.GetSessionByIdAsync(sessionId);
            if (session == null) return null;

            return new SessionResponseDto
            {
                SessionId = session.SessionId,
                Title = session.Title,
                Description = session.Description,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                Capacity = session.Capacity,
                IsApproved = session.IsApproved,
                TrainerId = session.TrainerId,
                TrainerName = session.Trainer != null
                    ? $"{session.Trainer.FirstName} {session.Trainer.LastName}"
                    : "Unknown",
                MeetingLink = session.MeetingLink  // include your new field
            };
        }


        public async Task<string> CreateSessionAsync(Session session, ClaimsPrincipal user)
        {
            var role = user.FindFirst(ClaimTypes.Role)?.Value ?? "User";
            var userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            session.TrainerId = userId; // link trainer/admin who creates
            session.CreatedAt = DateTime.UtcNow;

            if (role == "Trainer")
            {
                session.IsApproved = false; // needs admin approval
            }
            else if (role == "Admin")
            {
                session.IsApproved = true; // auto-approved
            }

            await _repository.AddSessionAsync(session);

            return role == "Trainer"
                ? "Session created. Waiting for admin approval."
                : "Session created and approved.";
        }

        public async Task<string> UpdateSessionAsync(SessionUpdateDto dto)
        {
            var session = await _repository.GetSessionByIdAsync(dto.SessionId);
            if (session == null) return "Session not found.";

            session.Title = dto.Title;
            session.Description = dto.Description;
            session.StartTime = dto.StartTime;
            session.EndTime = dto.EndTime;
            session.Capacity = dto.Capacity;
            session.IsApproved = dto.IsApproved;
            session.MeetingLink = dto.MeetingLink;

            await _repository.UpdateSessionAsync(session);

            return "Session updated successfully.";
        }

        public async Task<string> DeleteSessionAsync(int sessionId)
        {
            var session = await _repository.GetSessionByIdAsync(sessionId);
            if (session == null) return "Session not found.";

            await _repository.DeleteSessionAsync(session);
            return "Session deleted successfully.";
        }

        // ✅ Admin approval
        public async Task<string> ApproveSessionAsync(int sessionId, bool approve)
        {
            var session = await _repository.GetSessionByIdAsync(sessionId);
            if (session == null) return "Session not found.";

            session.IsApproved = approve;
            await _repository.UpdateSessionAsync(session);

            return approve
                ? "Session approved successfully."
                : "Session rejected.";
        }

    }

    }

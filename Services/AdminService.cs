using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Training_Session_Booking_Portal.DTOs;
using Training_Session_Booking_Portal.Interfaces;
using Training_Session_Booking_Portal.Models;
using Training_Session_Booking_Portal.Repositories.Interfaces;

namespace Training_Session_Booking_Portal.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repo;

        public AdminService(IAdminRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<SessionResponseDto>> GetPendingSessionsAsync()
        {
            // Fetch sessions with Status = Pending
            var sessions = await _repo.GetPendingSessionsAsync();

            // Map to SessionResponseDto
            var result = sessions.Select(s => new SessionResponseDto
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

            return result;
        }


        public async Task<bool> ApproveSessionAsync(int id)
        {
            var session = await _repo.GetSessionByIdAsync(id);
            if (session == null) return false;

            await _repo.ApproveSessionAsync(session);
            return true;
        }

        public async Task<bool> RejectSessionAsync(int id)
        {
            var session = await _repo.GetSessionByIdAsync(id);
            if (session == null) return false;

            await _repo.RejectSessionAsync(session);
            return true;
        }

        public async Task<bool> AddTrainerAsync(AddTrainerDto dto)
        {
            var existingUser = await _repo.GetUserByEmailAsync(dto.Email);
            if (existingUser != null) return false;

            var trainer = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.PasswordHash), // use local method
                RoleId = 2, // Assuming 2 = Trainer
                CreatedAt = DateTime.UtcNow
            };

            await _repo.AddTrainerAsync(trainer);
            return true;
        }


        public async Task<bool> PromoteUserToTrainerAsync(int userId)
        {
            var user = await _repo.GetUserByIdAsync(userId);
            if (user == null) return false;

            user.RoleId = 2; // Trainer role
            await _repo.UpdateUserAsync(user);
            return true;
        }

        public async Task<bool> DeleteTrainerAsync(int id)
        {
            var trainer = await _repo.GetUserByIdAsync(id);
            if (trainer == null) return false;

            await _repo.DeleteTrainerAsync(trainer);
            return true;
        }

        public async Task<int> GetPendingSessionsCountAsync()
        {
            return (await _repo.GetPendingSessionsAsync()).Count();
        }

        public async Task<int> GetApprovedSessionsCountAsync()
        {
            return await _repo.GetApprovedSessionsCountAsync();
        }

        public async Task<int> GetTrainersCountAsync()
        {
            return await _repo.GetTrainersCountAsync();
        }


        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

    }

}

using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Session>> GetPendingSessionsAsync() =>
            await _repo.GetPendingSessionsAsync();

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

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

    }

}

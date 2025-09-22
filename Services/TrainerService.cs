using Training_Session_Booking_Portal.DTOs;
using Training_Session_Booking_Portal.Interfaces;
using Training_Session_Booking_Portal.Models;
using Training_Session_Booking_Portal.Repositories.Interfaces;
using Training_Session_Booking_Portal.Services;

namespace Training_Session_Booking_Portal.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository _repository;

        public TrainerService(ITrainerRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> CreateSessionAsync(int trainerId, SessionDto dto)
        {
            var session = new Session
            {
                Title = dto.Title,
                Description = dto.Description,
                Capacity = dto.Capacity,
                TrainerId = trainerId,
                IsApproved = false,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            await _repository.CreateSessionAsync(session);
            return "Session created. Waiting for admin approval.";
        }

        public async Task<List<Session>> GetMySessionsAsync(int trainerId)
        {
            return await _repository.GetSessionsByTrainerAsync(trainerId);
        }
    }
}

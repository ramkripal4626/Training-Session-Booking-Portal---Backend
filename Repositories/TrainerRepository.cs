using Microsoft.EntityFrameworkCore;
using Training_Session_Booking_Portal.Data;
using Training_Session_Booking_Portal.Models;
using Training_Session_Booking_Portal.Repositories.Interfaces;

namespace Training_Session_Booking_Portal.Repositories
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly AppDbContext _context;

        public TrainerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Session> CreateSessionAsync(Session session)
        {
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task<List<Session>> GetSessionsByTrainerAsync(int trainerId)
        {
            return await _context.Sessions
                .Where(s => s.TrainerId == trainerId)
                .ToListAsync();
        }

    }
}

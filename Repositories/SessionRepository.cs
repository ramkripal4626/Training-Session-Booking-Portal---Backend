using Microsoft.EntityFrameworkCore;
using Training_Session_Booking_Portal.Data;
using Training_Session_Booking_Portal.Models;
using Training_Session_Booking_Portal.Repositories.Interfaces;

namespace Training_Session_Booking_Portal.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly AppDbContext _context;

        public SessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Session>> GetAllSessionsAsync()
        {
            return await _context.Sessions
                .Include(s => s.Trainer) // eager load trainer
                .ToListAsync();
        }

        public async Task<Session?> GetSessionByIdAsync(int sessionId)
        {
            return await _context.Sessions
                .Include(s => s.Trainer)
                .FirstOrDefaultAsync(s => s.SessionId == sessionId);
        }

        public async Task AddSessionAsync(Session session)
        {
            session.IsApproved = false; // every new session needs admin approval
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSessionAsync(Session session)
        {
            _context.Sessions.Update(session);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSessionAsync(Session session)
        {
            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
        }

    }

}

using Microsoft.EntityFrameworkCore;
using Training_Session_Booking_Portal.Data;
using Training_Session_Booking_Portal.Models;
using Training_Session_Booking_Portal.Repositories.Interfaces;

namespace Training_Session_Booking_Portal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Session>> GetAvailableSessionsAsync()
        {
            return await _context.Sessions
                .Where(s => s.IsApproved)
                .Include(s => s.Trainer)
                .ToListAsync();
        }

        public async Task<bool> IsAlreadyRegisteredAsync(int sessionId, int userId)
        {
            return await _context.Bookings.AnyAsync(b => b.SessionId == sessionId && b.UserId == userId);
        }

        public async Task<Session?> GetSessionByIdAsync(int sessionId)
        {
            return await _context.Sessions.FindAsync(sessionId);
        }

        public async Task<int> GetBookingCountAsync(int sessionId)
        {
            return await _context.Bookings.CountAsync(b => b.SessionId == sessionId);
        }

        public async Task AddBookingAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Booking>> GetUserBookingsAsync(int userId)
        {
            return await _context.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.Session)
                .ThenInclude(s => s.Trainer)
                .ToListAsync();
        }

        public async Task<Booking?> GetBookingAsync(int sessionId, int userId)
        {
            return await _context.Bookings
                .FirstOrDefaultAsync(b => b.UserId == userId && b.SessionId == sessionId);
        }

        public async Task RemoveBookingAsync(Booking booking)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }

}

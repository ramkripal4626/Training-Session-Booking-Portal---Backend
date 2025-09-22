using Microsoft.EntityFrameworkCore;
using Training_Session_Booking_Portal.Data;
using Training_Session_Booking_Portal.Interfaces;
using Training_Session_Booking_Portal.Models;
using Training_Session_Booking_Portal.Repositories.Interfaces;

namespace Training_Session_Booking_Portal.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Session>> GetPendingSessionsAsync()
        {
            return await _context.Sessions
                .Where(s => !s.IsApproved)
                .Include(s => s.Trainer)
                .ToListAsync();
        }

        public async Task<Session?> GetSessionByIdAsync(int id) =>
            await _context.Sessions.FindAsync(id);

        public async Task ApproveSessionAsync(Session session)
        {
            session.IsApproved = true;
            await _context.SaveChangesAsync();
        }

        public async Task RejectSessionAsync(Session session)
        {
            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id) =>
            await _context.Users.FindAsync(id);

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }


        public async Task AddTrainerAsync(User trainer)
        {
            _context.Users.Add(trainer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTrainerAsync(User trainer)
        {
            _context.Users.Remove(trainer);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetPendingSessionsCountAsync()
        {
            return await _context.Sessions.CountAsync(s => !s.IsApproved);
        }

        public async Task<int> GetApprovedSessionsCountAsync()
        {
            return await _context.Sessions.CountAsync(s => s.IsApproved);
        }

        public async Task<int> GetTrainersCountAsync()
        {
            return await _context.Users.CountAsync(u => u.RoleId == 2); // Assuming RoleId=2 is Trainer
        }

    }
}

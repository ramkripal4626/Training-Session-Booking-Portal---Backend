using Microsoft.EntityFrameworkCore;
using Training_Session_Booking_Portal.Models;

namespace Training_Session_Booking_Portal.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique constraint for Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Booking relationships
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Session)
                .WithMany(s => s.Bookings)
                .HasForeignKey(b => b.SessionId);

            // Session ↔ Trainer
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Trainer)
                .WithMany(u => u.SessionsAsTrainer)
                .HasForeignKey(s => s.TrainerId)
                .OnDelete(DeleteBehavior.Restrict); // prevents deleting trainer accidentally

            // 👉 Seed Roles

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "User" },
                new Role { RoleId = 2, RoleName = "Trainer" },
                new Role { RoleId = 3, RoleName = "Admin" }
            );

            modelBuilder.Entity<User>().HasData(
                new User {
                    UserId = 1,
                    FirstName = "System",
                    LastName = "Admin",
                    Email = "admin@example.com",
                    PasswordHash = "XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=",
                    RoleId = 3,
                    CreatedAt = new DateTime(2025, 09, 11)
                }
            );


        }


    }
}

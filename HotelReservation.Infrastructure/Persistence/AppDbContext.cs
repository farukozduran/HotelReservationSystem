using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels => Set<Hotel>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Reservation> Reservations => Set<Reservation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API
            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Rooms)
                .WithOne(h => h.Hotel)
                .HasForeignKey(r => r.HotelId);

            modelBuilder.Entity<Room>()
                .Property(r => r.Price)
                .HasPrecision(18, 2);
        }
    }
}

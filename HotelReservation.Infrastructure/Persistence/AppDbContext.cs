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
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<ReservationRoom> ReservationRooms => Set<ReservationRoom>();

        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();
        public DbSet<PaymentStatus> PaymentStatuses => Set<PaymentStatus>();

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Hotel -> Rooms
            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Rooms)
                .WithOne(h => h.Hotel)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Hotel -> Manager (User)
            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.ManagerUser)
                .WithMany()
                .HasForeignKey(h => h.ManagerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reservation -> User (Customer)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Reservations)
                .HasForeignKey(r => r.HotelId);

            // Room Price Precision
            modelBuilder.Entity<Room>()
                .Property(r => r.Price)
                .HasPrecision(18, 2);

            // ReservationRoom (Many-to-Many)
            modelBuilder.Entity<ReservationRoom>()
                .HasKey(rr => new { rr.ReservationId, rr.RoomId });

            modelBuilder.Entity<ReservationRoom>()
                .HasOne(rr => rr.Reservation)
                .WithMany(r => r.ReservationRooms)
                .HasForeignKey(rr => rr.ReservationId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReservationRoom>()
                .HasOne(rr => rr.Room)
                .WithMany(r => r.ReservationRooms)
                .HasForeignKey(rr => rr.RoomId)
                .OnDelete(DeleteBehavior.NoAction);

            // UserRole (Many-to-many)
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);


            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Reservation)
                .WithMany(r => r.Payments)
                .HasForeignKey(p => p.ReservationId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.PaymentMethod)
                .WithMany(pm => pm.Payments)
                .HasForeignKey(p => p.PaymentMethodId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.PaymentStatus)
                .WithMany(ps => ps.Payments)
                .HasForeignKey(p => p.PaymentStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            //  ROOM STATUS
            modelBuilder.Entity<RoomStatus>().HasData(
                new RoomStatus { Id = 1, Name = "Available", Description = "Available" },
                new RoomStatus { Id = 2, Name = "Occupied", Description = "Occupied" },
                new RoomStatus { Id = 3, Name = "Maintenance", Description = "Maintenance" }
            );

            //  ROOM TYPES
            modelBuilder.Entity<RoomType>().HasData(
                new RoomType { Id = 1, Name = "Single", Description = "Single Room", Capacity = 1 },
                new RoomType { Id = 2, Name = "Double", Description = "Double Room", Capacity = 2 }
            );

            //  ROLES
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Customer" }
            );

            //  USERS
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Admin",
                    Surname = "User",
                    LoginName = "admin",
                    Password = "123",
                    Telephone = "5551111111",
                    Email = "admin@test.com"
                },
                new User
                {
                    Id = 2,
                    Name = "Test",
                    Surname = "Customer",
                    LoginName = "customer",
                    Password = "123",
                    Telephone = "5552222222",
                    Email = "customer@test.com"
                }
            );

            //  USER ROLES
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserId = 1, RoleId = 1 },
                new UserRole { UserId = 2, RoleId = 2 }
            );

            //  HOTELS
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Code = "HTL1",
                    Name = "Istanbul Grand Hotel",
                    Phone = "02121234567",
                    RoomCount = 10,
                    City = "Istanbul",
                    PostalCode = "34000",
                    Email = "hotel1@test.com",
                    ManagerUserId = 1
                },
                new Hotel
                {
                    Id = 2,
                    Code = "HTL2",
                    Name = "Ankara Comfort Hotel",
                    Phone = "03121234567",
                    RoomCount = 10,
                    City = "Ankara",
                    PostalCode = "06000",
                    Email = "hotel2@test.com",
                    ManagerUserId = 1
                }
            );

            //  ROOMS
            var rooms = new List<Room>();
            int roomId = 1;

            // Hotel 1
            for (int i = 1; i <= 10; i++)
            {
                rooms.Add(new Room
                {
                    Id = roomId,
                    HotelId = 1,
                    RoomNumber = $"1{i:D2}",
                    RoomTypeId = i % 2 == 0 ? 2 : 1,
                    RoomStatusId = 1,
                    Price = i % 2 == 0 ? 2000 : 1500
                });
                roomId++;
            }

            // Hotel 2
            for (int i = 1; i <= 10; i++)
            {
                rooms.Add(new Room
                {
                    Id = roomId,
                    HotelId = 2,
                    RoomNumber = $"2{i:D2}",
                    RoomTypeId = i % 2 == 0 ? 2 : 1,
                    RoomStatusId = 1,
                    Price = i % 2 == 0 ? 1800 : 1300
                });
                roomId++;
            }

            modelBuilder.Entity<Room>().HasData(rooms);

            //  PAYMENT METHODS
            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod { Id = 1, Name = "Credit Card", Description = "Credit Card Payment" },
                new PaymentMethod { Id = 2, Name = "Cash", Description = "Cash Payment" }
            );

            //  PAYMENT STATUS
            modelBuilder.Entity<PaymentStatus>().HasData(
                new PaymentStatus { Id = 1, Name = "Pending" },
                new PaymentStatus { Id = 2, Name = "Completed" },
                new PaymentStatus { Id = 3, Name = "Failed" }
            );
        }
    }
}

using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;

        public ReservationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            if (checkInDate >= checkOutDate)
            {
                throw new ArgumentException("Check-out date must be after check-in date.");
            }

            if (checkInDate < DateTime.Today)
            {
                throw new ArgumentException("Check-in date cannot be in the past.");
            }

            var room = await _context.Rooms.Include(r => r.Reservations).FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null)
            {
                throw new ArgumentException("Room not found.");
            }

            foreach (var reservation in room.Reservations)
            {
                if ((checkInDate < reservation.EndDate) && (checkOutDate > reservation.StartDate))
                {
                    return false; // Room is not available
                }
            }

            return true;
        }

        public async Task<Reservation> CreateReservation(int customerId, int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            if (customerId <= 0 || roomId <= 0)
            {
                throw new ArgumentException("Customer ID and Room ID must be positive integers.");
            }

            if (!await IsRoomAvailable(roomId, checkInDate, checkOutDate))
            {
                throw new InvalidOperationException("The room is not available for the selected dates.");
            }

            var reservation = new Reservation
            {
                RoomId = roomId,
                CustomerId = customerId,
                StartDate = checkInDate,
                EndDate = checkOutDate
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }
    }
}

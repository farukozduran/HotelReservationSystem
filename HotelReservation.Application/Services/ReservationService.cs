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

        //public async Task<bool> IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate)
        //{
        //    if (checkInDate >= checkOutDate)
        //    {
        //        throw new ArgumentException("Check-out date must be after check-in date.");
        //    }

        //    if (checkInDate < DateTime.Today)
        //    {
        //        throw new ArgumentException("Check-in date cannot be in the past.");
        //    }

        //    if (roomId == 0)
        //    {
        //        throw new ArgumentException("Room Id must be a positive integer.");
        //    }

        //    return !await _context.Reservations
        //        .AnyAsync(res =>
        //            res.RoomId == roomId &&
        //            checkInDate < res.EndDate &&
        //            checkOutDate > res.StartDate
        //        );
        //}

        //public async Task<Reservation> CreateReservation(int customerId, int roomId, DateTime checkInDate, DateTime checkOutDate)
        //{
        //    if (customerId <= 0 || roomId <= 0)
        //    {
        //        throw new ArgumentException("Customer ID and Room ID must be positive integers.");
        //    }

        //    if (checkInDate >= checkOutDate)
        //    {
        //        throw new ArgumentException("Invalid date range!");
        //    }

        //    if (checkInDate < DateTime.Today)
        //    {
        //        throw new ArgumentException("Check-in date cannot be in the past.");
        //    }

        //    // Transaction
        //    using var transaction = await _context.Database.BeginTransactionAsync();

        //    try
        //    {
        //        // DB Level Check

        //        var hasConflict = await _context.Reservations
        //            .AnyAsync(res =>
        //                res.RoomId == roomId &&
        //                checkInDate < res.EndDate &&
        //                checkOutDate > res.StartDate);

        //        if (hasConflict)
        //        {
        //            throw new InvalidOperationException("The room is not available for the selected dates.");
        //        }

        //        var reservation = new Reservation
        //        {
        //            RoomId = roomId,
        //            CustomerId = customerId,
        //            StartDate = checkInDate,
        //            EndDate = checkOutDate
        //        };

        //        _context.Reservations.Add(reservation);
        //        await _context.SaveChangesAsync();

        //        // COMMIT
        //        await transaction.CommitAsync();

        //        return reservation;

        //    }
        //    catch
        //    {
        //        // If there is an error ROLLBACK
        //        await transaction.RollbackAsync();
        //        throw;
        //    }
        //}

        //public async Task<List<Room>> GetAvailableRooms(int hotelId, DateTime checkInDate, DateTime checkOutDate)
        //{
        //    return await _context.Rooms
        //        .Where(r => r.HotelId == hotelId)
        //        .Where(r => !_context.Reservations.Any(res =>
        //            res.RoomId == r.Id &&
        //            checkInDate < res.EndDate &&
        //            checkOutDate > res.StartDate
        //        ))
        //        .ToListAsync();
        //}
    }
}

using HotelReservation.Application.DTOs;
using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using System.Linq.Expressions;

namespace HotelReservation.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;

        public ReservationService(AppDbContext context)
        {
            _context = context;
        }

        private Expression<Func<Reservation, bool>> DateOverlap(DateTime checkIn, DateTime checkOut)
        {
            return res => checkIn < res.EndDate && checkOut > res.StartDate;
        }
                
        public async Task<Reservation> CreateReservation(CreateReservationRequest request)
        {
            if (request.CustomerId <= 0)
            {
                throw new ArgumentException("Invalid Customer ID!");
            }

            if (request.RoomIds == null || !request.RoomIds.Any())
            {
                throw new ArgumentException("At least one room must be selected!");
            }

            if (request.CheckInDate >= request.CheckOutDate)
            {
                throw new ArgumentException("Invalid date range!");
            }

            if (request.CheckInDate < DateTime.Today)
            {
                throw new ArgumentException("Check-in date cannot be in the past.");
            }

            // Transaction
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // DB Level Check

                var roomIds = request.RoomIds.Distinct().ToList();

                var rooms = await _context.Rooms
                    .Where(r => roomIds.Contains(r.Id))
                    .ToListAsync();

                if (rooms.Select(r => r.HotelId).Distinct().Count() > 1)
                {
                    throw new Exception("Rooms must belong to the same hotel!");
                }

                if(rooms.Any(r => r.RoomStatusId != 1))
                {
                    throw new Exception("One or more rooms are not available.");
                }
                
                var conflictingRoomIds = await _context.Reservations
                    .Where(DateOverlap(request.CheckInDate, request.CheckOutDate))
                    .SelectMany(res => res.ReservationRooms)
                    .Where(rr => roomIds.Contains(rr.RoomId))
                    .Select(rr => rr.RoomId)
                    .Distinct()
                    .ToListAsync();

                if (conflictingRoomIds.Any())
                {
                    throw new InvalidOperationException(
                        $"Rooms not available: {string.Join(", ", conflictingRoomIds)}");
                }

                var reservation = new Reservation
                {
                    CustomerId = request.CustomerId,
                    StartDate = request.CheckInDate,
                    EndDate = request.CheckOutDate,
                    CreatedAt = DateTime.UtcNow,
                    Status = ReservationStatus.Pending
                };

                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();

                foreach (var roomId in request.RoomIds)
                {
                    _context.ReservationRooms.Add(new ReservationRoom
                    {
                        ReservationId = reservation.Id,
                        RoomId = roomId
                    });
                }

                await _context.SaveChangesAsync();

                // COMMIT
                await transaction.CommitAsync();

                return reservation;

            }
            catch
            {
                // If there is an error ROLLBACK
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<RoomDto>> GetAvailableRooms(int hotelId, DateTime checkInDate, DateTime checkOutDate)
        {
            if(checkInDate >= checkOutDate)
            {
                throw new ArgumentException("Invalid date range");
            }

            return await _context.Rooms
                .Where(r => r.HotelId == hotelId)
                .Where(r => r.RoomStatusId == 1)
                .Where(r => !_context.Reservations
                    .Where(DateOverlap(checkInDate, checkOutDate))
                    .Any(res =>
                        res.ReservationRooms.Any(rr => rr.RoomId == r.Id)))
                .Select(r => new RoomDto
                {
                    Id = r.Id,
                    RoomNumber = r.RoomNumber,
                    Price = r.Price,
                    RoomType = r.RoomType.Name,
                    RoomStatus = r.RoomStatus.Name
                })
                .ToListAsync();
        }
    }
}

using HotelReservation.Application.DTOs;
using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.Interfaces
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservation(CreateReservationRequest request);
        Task<List<Room>> GetAvailableRooms(int hotelId, DateTime checkInDate, DateTime checkOutDate);
    }
}

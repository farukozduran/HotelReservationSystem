using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.Interfaces
{
    public interface IReservationService
    {
        //Task<bool> IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate);
        Task<Reservation> CreateReservation(int customerId, int roomId, DateTime checkInDate, DateTime checkOutDate);
        Task<List<Room>> GetAvailableRooms(int hotelId, DateTime checkInDate, DateTime checkOutDate);
    }
}

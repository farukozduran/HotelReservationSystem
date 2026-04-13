namespace HotelReservation.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
        public int RoomStatusId { get; set; }

        public decimal Price { get; set; }

        public Hotel Hotel { get; set; }
        public RoomType RoomType { get; set; }
        public RoomStatus RoomStatus { get; set; }

        public ICollection<ReservationRoom> ReservationRooms { get; set; }
    }
}

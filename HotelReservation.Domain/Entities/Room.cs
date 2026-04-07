namespace HotelReservation.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string RoomNumber { get; set; } = null!;
        public int RoomTypeId { get; set; }
        public int RoomStatusId { get; set; }

        public decimal Price { get; set; }

        // Navigation Properties
        public Hotel Hotel { get; set; } = null!;
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}

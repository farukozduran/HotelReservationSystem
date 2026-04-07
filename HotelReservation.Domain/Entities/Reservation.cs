namespace HotelReservation.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        public int HotelId { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int RoomCount { get; set; }

        // Navigation Properties
        public Room Room { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
    }
}

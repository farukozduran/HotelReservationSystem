namespace HotelReservation.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        public int HotelId { get; set; }
        public int CustomerId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Status { get; set; } // Pending, Confirmend, Cancelled
        public DateTime CreatedAt { get; set; }

        public Hotel Hotel { get; set; }
        public User Customer { get; set; }

        public ICollection<ReservationRoom> ReservationRooms { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}

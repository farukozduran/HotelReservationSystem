namespace HotelReservation.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string UserCode { get; set; } = null!;

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}

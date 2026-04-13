namespace HotelReservation.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int PaymentMethodId { get; set; }
        public int PaymentStatusId { get; set; }

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}

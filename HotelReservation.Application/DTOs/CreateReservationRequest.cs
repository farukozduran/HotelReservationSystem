namespace HotelReservation.Application.DTOs
{
    public class CreateReservationRequest
    {
        public int CustomerId { get; set; }
        public List<int> RoomIds{ get; set; }
        public DateTime CheckInDate {  get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}

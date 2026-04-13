namespace HotelReservation.Domain.Entities
{
    public class RoomStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}

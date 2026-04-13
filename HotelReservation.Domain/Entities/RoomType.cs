namespace HotelReservation.Domain.Entities
{
    public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Capacity { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}

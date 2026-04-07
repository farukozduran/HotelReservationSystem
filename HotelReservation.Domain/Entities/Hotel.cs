namespace HotelReservation.Domain.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int RoomCount { get; set; }
        public string City { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ManagerCode { get; set; } = null!;

        // Navigation Property
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}

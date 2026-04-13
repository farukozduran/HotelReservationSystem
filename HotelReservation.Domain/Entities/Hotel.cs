namespace HotelReservation.Domain.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int RoomCount { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }

        public int ManagerUserId { get; set; }


        public User ManagerUser { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}

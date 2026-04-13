namespace HotelReservation.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}

using HotelReservation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Controllers
{
    public class ReservationController : Controller
    {
        public readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("reserve")]
        public async Task<IActionResult> ReserveRoom(int customerId, int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            try
            {
                var reservation = await _reservationService.CreateReservation(customerId, roomId, checkInDate, checkOutDate);
                return Ok(reservation);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

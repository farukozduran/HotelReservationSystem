using HotelReservation.Application.DTOs;
using HotelReservation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        public readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<IActionResult> ReserveRoom([FromBody] CreateReservationRequest request)
        {
            try
            {
                var reservation = await _reservationService.CreateReservation(request);
                return Ok(reservation);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("available-rooms")]
        public async Task<IActionResult> GetAvailableRooms(int hotelId, DateTime checkInDate, DateTime checkOutDate)
        {
            var availableRooms = await _reservationService.GetAvailableRooms(hotelId, checkInDate, checkOutDate);

            return Ok(availableRooms);
        }
    }
}

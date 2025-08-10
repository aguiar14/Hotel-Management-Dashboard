using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BookingController: ControllerBase
    {
        private readonly ILogger<BookingController> logger;
        private readonly IBookingRepository _bookingRepository;

        public BookingController(ILogger<BookingController> logger, IBookingRepository bookingRepository)
        {
            this.logger = logger;
            _bookingRepository = bookingRepository;
        }


        [HttpPost]
        public async Task<ActionResult<Booking>> Add(Booking booking)
        {
                try
            {
                var newBooking = await _bookingRepository.CreateBookingAsync(booking);
                return Created(newBooking.Id.ToString(), newBooking);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Error creating booking");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<Booking>>> Get(
            [FromQuery] int roomId = -1, 
            [FromQuery] int userId = -1, 
            [FromQuery] DateTime? checkInDate = null, 
            [FromQuery] DateTime? checkOutDate = null, 
            [FromQuery] string status = "")
        {
            var bookings = await _bookingRepository.GetBookingsAsync(roomId, userId, checkInDate ?? default(DateTime), checkOutDate ?? default(DateTime), status);
            return Ok(bookings);
        }

        [HttpGet("{bookingId}")]

        public async Task<ActionResult<Booking>>GetById(int bookingId)
        {
            var existingBokking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            return existingBokking == null ? NotFound() : Ok(existingBokking);
        }

        [HttpPut("{bookingId}")]
        public async Task<ActionResult<Booking?>> Update(Booking booking, int bookingId)
        {
            if (bookingId != booking.Id)
            {
                return BadRequest("Booking ID mismatch");
            }
            var updated = await _bookingRepository.UpdateBookingAsync(booking);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpPut("{bookingId}/confirm")]
        public async Task<ActionResult<bool>> Confirm(int bookingId)
        {
            var confirmed = await _bookingRepository.ConfirmBookingAsync(bookingId);
            return confirmed ? Ok(true) : NotFound();
        }

        [HttpPut("{bookingId}/cancel")]
        public async Task<ActionResult<bool>> Cancel(int bookingId)
        {
            var cancelled = await _bookingRepository.CancelBookingAsync(bookingId);
            return cancelled ? Ok(true) : NotFound();
        }

        }
}

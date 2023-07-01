using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for bookings.
    /// </summary>
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase {
        private ApplicationContext context;
        public BookingController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all bookings.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Booking (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Booking[]> GetAllBookings() {
            return Ok(context.Bookings.ToArray());
        }

        /// <summary>
        /// Returns the booking with a given id.
        /// </summary>
        /// <param name="bid">BookingID</param>
        [HttpGet("{bid}")]
        [SwaggerOperation(Tags = new[] { "Booking (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Booking> GetBooking ([FromRoute] int bid) {
            var value = context.Bookings.Where(v => v.BookingId == bid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a booking.
        /// </summary>
        /// <param name="value">new Booking</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Booking (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Booking>> AddBooking([FromBody] Booking value) {
            if (ModelState.IsValid) {
                //test if booking already exists
                if (context.Bookings.Where(v => v.BookingId == value.BookingId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Booking already exists");
                    return Conflict(ModelState); //booking with id already exists, we return a conflict
                }

                context.Bookings.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the booking
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of booking
        }

        /// <summary>
        /// Updates a booking.
        /// </summary>
        /// <param name="bid">BookingId</param>
        /// <param name="value">new Booking</param>
        [HttpPut("{bid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Booking (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Booking>> UpdateBooking([FromRoute] int bid, [FromBody] Booking value) {
            if (ModelState.IsValid) {
                var toUpdate = context.Bookings.Where(v => v.BookingId == bid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.Number = value.Number;
                    toUpdate.Date = value.Date;
                    toUpdate.TotalPrice  = value.TotalPrice;
                    toUpdate.CustomerId = value.CustomerId;
                    toUpdate.InvoiceId = value.InvoiceId;
                    toUpdate.PaymentMethodId = value.PaymentMethodId;
                    toUpdate.StatusId = value.StatusId;


                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Delete a booking.
        /// </summary>
        /// <param name="bid">BookingId</param>
        [HttpDelete("{bid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Booking (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Booking>> DeleteBooking([FromRoute] int bid) {
            var toDelete = context.Bookings.Where(v => v.BookingId == bid);
            context.Bookings.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
		
    }
}
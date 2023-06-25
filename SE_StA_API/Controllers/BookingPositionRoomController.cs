using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for booking position rooms.
    /// </summary>
    [Route("api/bookingPositionRooms")]
    [ApiController]
    public class BookingPositionRoomController : ControllerBase {
		private ApplicationContext context;
        public BookingPositionRoomController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all booking position rooms.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "BookingPositionRoom (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<BookingPositionRoom[]> GetAllBookingPositionRooms() {
            return Ok(context.BookingPositionRooms.ToArray());
        }

        /// <summary>
        /// Returns the booking position room with a given id.
        /// </summary>
        /// <param name="bprid">BookingPositionRoomID</param>
        [HttpGet("{bprid}")]
        [SwaggerOperation(Tags = new[] { "BookingPositionRoom (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BookingPositionRoom> GetBookingPositionRoom ([FromRoute] int bprid) {
            var value = context.BookingPositionRooms.Where(v => v.BookingPositionRoomId == bprid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a Booking Position Room.
        /// </summary>
        /// <param name="value">new Booking Position Room</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Booking Position Room (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<BookingPositionRoom>> AddBookingPositionRoom([FromBody] BookingPositionRoom value) {
            if (ModelState.IsValid) {
                //test if Booking Position Room already exists
                if (context.BookingPositionRooms.Where(v => v.BookingPositionRoomId == value.BookingPositionRoomId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "BookingPositionRoom already exists");
                    return Conflict(ModelState); //booking position room with id already exists, we return a conflict
                }

                context.BookingPositionRooms.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the booking position room
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of booking position room
        }

        /// <summary>
        /// Updates a booking position room.
        /// </summary>
        /// <param name="bprid">BookingPositionRoomId</param>
        /// <param name="value">new BookingPositionRoom</param>
        [HttpPut("{bprid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Booking Position Room (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingPositionRoom>> UpdateBookingPositionRoom([FromRoute] int bprid, [FromBody] BookingPositionRoom value) {
            if (ModelState.IsValid) {
                var toUpdate = context.BookingPositionRooms.Where(v => v.BookingPositionRoomId == bprid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.Date = value.Date;
                    toUpdate.Price = value.Price;
                    toUpdate.BookingId = value.BookingId;
                    toUpdate.RoomId = value.RoomId;

                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Delete a booking position room.
        /// </summary>
        /// <param name="bprid">BookingPositionRoomId</param>
        [HttpDelete("{bprid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Booking Position Room (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<BookingPositionRoom>> DeleteBookingPositionRoom([FromRoute] int bprid) {
            var toDelete = context.BookingPositionRooms.Where(v => v.BookingPositionRoomId == bprid);
            context.BookingPositionRooms.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for booking positions servoces.
    /// </summary>
    [Route("api/bookingPositionServioces")]
    [ApiController]
    public class BookingPositionServiceController : ControllerBase {
		private ApplicationContext context;
        public BookingPositionServiceController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all booking position services.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "BookingPositionServices (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<BookingPositionService[]> GetAllBookingPositionServices() {
            return Ok(context.BookingPositionServices.ToArray());
        }

        /// <summary>
        /// Returns the booking position service with a given id.
        /// </summary>
        /// <param name="bpsid">BookingPositionServiceID</param>
        [HttpGet("{bpsid}")]
        [SwaggerOperation(Tags = new[] { "BookingPositionService (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BookingPositionService> GetBookingPositionService ([FromRoute] int bpsid) {
            var value = context.BookingPositionServices.Where(v => v.BookingPositionServiceId == bpsid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a Booking Position Service.
        /// </summary>
        /// <param name="value">new Booking Position Service</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Booking Position Service (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<BookingPositionService>> AddBookingPositionService([FromBody] BookingPositionService value) {
            if (ModelState.IsValid) {
                //test if Booking Position Service already exists
                if (context.BookingPositionServices.Where(v => v.BookingPositionServiceId == value.BookingPositionServiceId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "BookingPositionService already exists");
                    return Conflict(ModelState); //booking position service with id already exists, we return a conflict
                }

                context.BookingPositionServices.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the booking position service
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of booking position service
        }

        /// <summary>
        /// Updates a booking position service.
        /// </summary>
        /// <param name="bpsid">BookingPositionServiceId</param>
        /// <param name="value">new BookingPositionService</param>
        [HttpPut("{bpsid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Booking Position Service (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingPositionService>> UpdateBookingPositionService([FromRoute] int bpsid, [FromBody] BookingPositionService value) {
            if (ModelState.IsValid) {
                var toUpdate = context.BookingPositionServices.Where(v => v.BookingPositionServiceId == bpsid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.DateTime = value.DateTime;
                    toUpdate.Price = value.Price;
                    toUpdate.BookingId = value.BookingId;
                    toUpdate.ServiceId = value.ServiceId;

                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Delete a booking position service.
        /// </summary>
        /// <param name="bpsid">BookingPositionServiceId</param>
        [HttpDelete("{bpsid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Booking Position Service (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<BookingPositionService>> DeleteBookingPositionService([FromRoute] int bpsid) {
            var toDelete = context.BookingPositionServices.Where(v => v.BookingPositionServiceId == bpsid);
            context.BookingPositionServices.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for flights.
    /// </summary>
    [Route("api/flights")]
    [ApiController]
    public class FlightController : ControllerBase {
		private ApplicationContext context;
        public FlightController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all flights.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Flights (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Flight[]> GetAllFlights() {
            return Ok(context.Flights.ToArray());
        }

        /// <summary>
        /// Returns the flight with a given id.
        /// </summary>
        /// <param name="fid">FlightID</param>
        [HttpGet("{fid}")]
        [SwaggerOperation(Tags = new[] { "Flight (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Flight> GetFlight ([FromRoute] int fid) {
            var value = context.Flights.Where(v => v.FlightId == fid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a flight.
        /// </summary>
        /// <param name="value">new flight</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Flight (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Flight>> AddFlight([FromBody] Flight value) {
            if (ModelState.IsValid) {
                //test if flight already exists
                if (context.Flights.Where(v => v.FlightId == value.FlightId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Flight already exists");
                    return Conflict(ModelState); //flight with id already exists, we return a conflict
                }

                context.Flights.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the flight
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of flight
        }

        /// <summary>
        /// Updates a flight.
        /// </summary>
        /// <param name="fid">FlightId</param>
        /// <param name="value">new Flight</param>
        [HttpPut("{fid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Flight (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Flight>> UpdateFlight([FromRoute] int fid, [FromBody] Flight value) {
            if (ModelState.IsValid) {
                var toUpdate = context.Flights.Where(v => v.FlightId == fid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.FlightNumber = value.FlightNumber;
                    toUpdate.Destination = value.Destination;
                    toUpdate.ServiceId  = value.ServiceId;
                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a flight.
        /// </summary>
        /// <param name="fid">FlightId</param>
        [HttpDelete("{fid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Flight (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Flight>> DeleteFlight([FromRoute] int fid) {
            var toDelete = context.Flights.Where(v => v.FlightId == fid);
            context.Flights.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
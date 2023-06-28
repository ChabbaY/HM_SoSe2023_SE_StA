using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for time zones.
    /// </summary>
    [Route("api/timeZones")]
    [ApiController]
    public class TimeZoneController : ControllerBase {
       	private ApplicationContext context;
        public TimeZoneController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all time zones.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Time Zone (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<DataObject.TimeZone[]> GetAllTimeZones() {
            return Ok(context.TimeZones.ToArray());
        }

        /// <summary>
        /// Returns the time zones with a given id.
        /// </summary>
        /// <param name="tzid">TimeZoneID</param>
        [HttpGet("{tzid}")]
        [SwaggerOperation(Tags = new[] { "Time Zone (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TimeZoneController> GetTimeZone ([FromRoute] int tzid) {
            var value = context.TimeZones.Where(v => v.TimeZoneId == tzid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a time zone.
        /// </summary>
        /// <param name="value">new time zone</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Time Zone (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<TimeZoneController>> AddTimeZone ([FromBody] DataObject.TimeZone value) {
            if (ModelState.IsValid) {
                //test if time zone already exists
                if (context.TimeZones.Where(v => v.TimeZoneId == value.TimeZoneId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Time Zone already exists");
                    return Conflict(ModelState); //time zone with id already exists, we return a conflict
                }

                context.TimeZones.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the time zone
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of time zone
        }

        /// <summary>
        /// Updates a time zone.
        /// </summary>
        /// <param name="tzid">TimeZoneId</param>
        /// <param name="value">new TimeZone</param>
        [HttpPut("{tzid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Time Zone (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DataObject.TimeZone>> UpdateTimeZone([FromRoute] int tzid, [FromBody] DataObject.TimeZone value) {
            if (ModelState.IsValid) {
                var toUpdate = context.TimeZones.Where(v => v.TimeZoneId == tzid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.Name = value.Name;
                    toUpdate.difUtc = value.difUtc;
                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a time zone.
        /// </summary>
        /// <param name="tzid">TimeZoneId</param>
        [HttpDelete("{tzid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Time Zone (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<DataObject.TimeZone>> DeleteTimeZone([FromRoute] int tzid) {
            var toDelete = context.TimeZones.Where(v => v.TimeZoneId == tzid);
            context.TimeZones.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
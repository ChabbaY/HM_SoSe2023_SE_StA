using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for country time zones.
    /// </summary>
    [Route("api/countryTimeZones")]
    [ApiController]
    public class CountryTimeZoneController : ControllerBase {
		private ApplicationContext context;
        public CountryTimeZoneController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all country time zones.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Country Time Zone (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CountryTimeZone[]> GetAllCountryTimeZones() {
            return Ok(context.CountryTimeZones.ToArray());
        }

        /// <summary>
        /// Returns the country time zones with a given id.
        /// </summary>
        /// <param name="ctzid">CountryTimeZoneID</param>
        [HttpGet("{ctzid}")]
        [SwaggerOperation(Tags = new[] { "Country Time Zone (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CountryTimeZoneController> GetCountryTimeZone ([FromRoute] int ctzid) {
            var value = context.CountryTimeZones.Where(v => v.CountryTimeZoneId == ctzid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a country time zone.
        /// </summary>
        /// <param name="value">new country time zone</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Country Time Zone (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CountryTimeZoneController>> AddCountryTimeZone ([FromBody] CountryTimeZone value) {
            if (ModelState.IsValid) {
                //test if country time zone already exists
                if (context.CountryTimeZones.Where(v => v.CountryTimeZoneId == value.CountryTimeZoneId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Country Time Zone already exists");
                    return Conflict(ModelState); //country time zone with id already exists, we return a conflict
                }

                context.CountryTimeZones.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the country time zone
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of country time zone
        }

        /// <summary>
        /// Updates a country time zone.
        /// </summary>
        /// <param name="ctzid">CountryTimeZoneId</param>
        /// <param name="value">new CountryTimeZone</param>
        [HttpPut("{ctzid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Country Time Zone (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CountryTimeZone>> UpdateCountryTimeZone([FromRoute] int ctzid, [FromBody] CountryTimeZone value) {
            if (ModelState.IsValid) {
                var toUpdate = context.CountryTimeZones.Where(v => v.CountryTimeZoneId == ctzid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.CountryId = value.CountryId;
                    toUpdate.TimeZoneId = value.TimeZoneId;
                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a country time zone.
        /// </summary>
        /// <param name="ctzid">CountryTimeZoneId</param>
        [HttpDelete("{ctzid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Country Time Zone (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CountryTimeZone>> DeleteCountryTimeZone([FromRoute] int ctzid) {
            var toDelete = context.CountryTimeZones.Where(v => v.CountryTimeZoneId == ctzid);
            context.CountryTimeZones.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
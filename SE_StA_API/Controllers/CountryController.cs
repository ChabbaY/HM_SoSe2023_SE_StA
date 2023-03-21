using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for countries.
    /// </summary>
    [Route("api/countries")]
    [ApiController]
    public class CountryController : ControllerBase {
		private ApplicationContext context;
        public CountryController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all countries.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Country (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Country[]> GetAllCountries() {
            return Ok(context.Countries.ToArray());
        }

        /// <summary>
        /// Returns the country with a given id.
        /// </summary>
        /// <param name="cid">CountryId</param>
        [HttpGet("{cid}")]
        [SwaggerOperation(Tags = new[] { "Country (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Country> GetCountry([FromRoute] int cid) {
            var value = context.Countries.Where(v => v.Id == cid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a country.
        /// </summary>
        /// <param name="value">new Country</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Country (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Country>> AddCountry([FromBody] Country value) {
            if (ModelState.IsValid) {
                //test if country already exists
                if (context.Countries.Where(v => v.Id == value.Id).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Country already exists");
                    return Conflict(ModelState); //country with id already exists, we return a conflict
                }

                context.Countries.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the country
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of Country
        }

        /// <summary>
        /// Updates a country.
        /// </summary>
        /// <param name="cid">CountryId</param>
        /// <param name="value">new Country</param>
        [HttpPut("{cid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Country (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Country>> UpdateCountry([FromRoute] int cid, [FromBody] Country value) {
            if (ModelState.IsValid) {
                var toUpdate = context.Countries.Where(v => v.Id == cid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.Name = value.Name;
                    toUpdate.Language = value.Language;
                    toUpdate.Iso2 = value.Iso2;

                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Delete a country.
        /// </summary>
        /// <param name="cid">CountryId</param>
        [HttpDelete("{cid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Country (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Country>> DeleteCountry([FromRoute] int cid) {
            var toDelete = context.Countries.Where(v => v.Id == cid);
            context.Countries.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
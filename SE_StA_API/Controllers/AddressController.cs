using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for addresses.
    /// </summary>
    [Route("api/addresses")]
    [ApiController]
    public class AddressController : ControllerBase {
	
		private ApplicationContext context;
        public AddressController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all addresses.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Address (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Address[]> GetAllAddresses() {
            return Ok(context.Addresses.ToArray());
        }

        /// <summary>
        /// Returns the address with a given id.
        /// </summary>
        /// <param name="aid">AddressID</param>
        [HttpGet("{aid}")]
        [SwaggerOperation(Tags = new[] { "Address (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Address> GetAddress([FromRoute] int aid) {
            var value = context.Addresses.Where(v => v.AddressId == aid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds an address.
        /// </summary>
        /// <param name="value">new Adress</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Address (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Address>> AddAddress([FromBody] Address value) {
            if (ModelState.IsValid) {
                //test if address already exists
                if (context.Addresses.Where(v => v.AddressId == value.AddressId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Address already exists");
                    return Conflict(ModelState); //address with id already exists, we return a conflict
                }

                context.Addresses.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the address
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of Address
        }

        /// <summary>
        /// Updates an address.
        /// </summary>
        /// <param name="aid">AddressId</param>
        /// <param name="value">new Address</param>
        [HttpPut("{aid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Address (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Address>> UpdateAddress([FromRoute] int aid, [FromBody] Address value) {
            if (ModelState.IsValid) {
                var toUpdate = context.Addresses.Where(v => v.AddressId == aid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.Street = value.Street;
                    toUpdate.HouseNumber = value.HouseNumber;
                    toUpdate.PostalCode = value.PostalCode;
                    toUpdate.Town = value.Town;
                    toUpdate.AddressAddition = value.AddressAddition;
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
        /// Delete an address.
        /// </summary>
        /// <param name="aid">AddressId</param>
        [HttpDelete("{aid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Address (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Address>> DeleteAddress([FromRoute] int aid) {
            var toDelete = context.Addresses.Where(v => v.AddressId == aid);
            context.Addresses.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
 }
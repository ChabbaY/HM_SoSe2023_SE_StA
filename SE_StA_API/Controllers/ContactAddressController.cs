using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for contact addresses.
    /// </summary>
    [Route("api/contactAddresses")]
    [ApiController]
    public class ContactAddressController : ControllerBase {
		private ApplicationContext context;
        public ContactAddressController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all contact addresses.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Contact Address (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ContactAddress[]> GetAllContactAddress() {
            return Ok(context.ContactAddresses.ToArray());
        }

        /// <summary>
        /// Returns the contact addresses with a given id.
        /// </summary>
        /// <param name="caid">ContactAddressID</param>
        [HttpGet("{caid}")]
        [SwaggerOperation(Tags = new[] { "Contact Address (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ContactAddress> GetContactAddress ([FromRoute] int caid) {
            var value = context.ContactAddresses.Where(v => v.ContactAddressId == caid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a contact address.
        /// </summary>
        /// <param name="value">new contact address</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Contact Address(Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ContactAddress>> AddContactAddress([FromBody] ContactAddress value) {
            if (ModelState.IsValid) {
                //test if contact address already exists
                if (context.ContactAddresses.Where(v => v.ContactAddressId == value.ContactAddressId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Contact Address already exists");
                    return Conflict(ModelState); //contact address with id already exists, we return a conflict
                }

                context.ContactAddresses.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the contact address
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of contact address
        }

        /// <summary>
        /// Updates a contact address.
        /// </summary>
        /// <param name="caid">ContactAddressId</param>
        /// <param name="value">new ContactAddress</param>
        [HttpPut("{caid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Contact Address (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContactAddress>> UpdateContactAddress([FromRoute] int caid, [FromBody] ContactAddress value) {
            if (ModelState.IsValid) {
                var toUpdate = context.ContactAddresses.Where(v => v.ContactAddressId == caid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.ContactId = value.ContactId;
                    toUpdate.AddressId = value.AddressId;
                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a contact address.
        /// </summary>
        /// <param name="caid">ContactAddressId</param>
        [HttpDelete("{caid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Contact Address (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ContactAddress>> DeleteContactAddress([FromRoute] int caid) {
            var toDelete = context.ContactAddresses.Where(v => v.ContactAddressId == caid);
            context.ContactAddresses.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
		
    }
}
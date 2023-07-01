using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for contacts.
    /// </summary>
    [Route("api/contacts")]
    [ApiController]
    public class ContactController : ControllerBase {
		private ApplicationContext context;
        public ContactController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all contacts.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Contact (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Contact[]> GetAllContacts() {
            return Ok(context.Contacts.ToArray());
        }

        /// <summary>
        /// Returns the contact with a given id.
        /// </summary>
        /// <param name="cid">ContactID</param>
        [HttpGet("{cid}")]
        [SwaggerOperation(Tags = new[] { "Contact (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Contact> GetContact ([FromRoute] int cid) {
            var value = context.Contacts.Where(v => v.ContactId == cid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a contact.
        /// </summary>
        /// <param name="value">new contact</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Contact (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Contact>> AddContact([FromBody] Contact value) {
            if (ModelState.IsValid) {
                //test if contact already exists
                if (context.Contacts.Where(v => v.ContactId == value.ContactId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Contact already exists");
                    return Conflict(ModelState); //contact with id already exists, we return a conflict
                }

                context.Contacts.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the contact
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of contact
        }

        /// <summary>
        /// Updates a contact.
        /// </summary>
        /// <param name="cid">ContactId</param>
        /// <param name="value">new Contact</param>
        [HttpPut("{cid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Contact (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Contact>> UpdateContact([FromRoute] int cid, [FromBody] Contact value) {
            if (ModelState.IsValid) {
                var toUpdate = context.Contacts.Where(v => v.ContactId == cid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.Salutation = value.Salutation;
                    toUpdate.Phone = value.Phone;
                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a contact.
        /// </summary>
        /// <param name="cid">ContactId</param>
        [HttpDelete("{cid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Contact (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Contact>> DeleteContact([FromRoute] int cid) {
            var toDelete = context.Contacts.Where(v => v.ContactId == cid);
            context.Contacts.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
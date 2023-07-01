using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for services.
    /// </summary>
    [Route("api/services")]
    [ApiController]
    public class ServiceController : ControllerBase {
        private ApplicationContext context;
        public ServiceController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all services.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Services (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Service[]> GetAllServices() {
            return Ok(context.Services.ToArray());
        }

        /// <summary>
        /// Returns the service with a given id.
        /// </summary>
        /// <param name="sid">ServiceID</param>
        [HttpGet("{sid}")]
        [SwaggerOperation(Tags = new[] { "Service (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Service> GetService ([FromRoute] int sid) {
            var value = context.Services.Where(v => v.ServiceId == sid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a service.
        /// </summary>
        /// <param name="value">new service</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Service (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Service>> AddService([FromBody] Service value) {
            if (ModelState.IsValid) {
                //test if service already exists
                if (context.Services.Where(v => v.ServiceId == value.ServiceId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Service already exists");
                    return Conflict(ModelState); //service with id already exists, we return a conflict
                }

                context.Services.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the service
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of service
        }

        /// <summary>
        /// Updates a service.
        /// </summary>
        /// <param name="sid">ServiceId</param>
        /// <param name="value">new Service</param>
        [HttpPut("{sid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Service (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Service>> UpdateService([FromRoute] int sid, [FromBody] Service value) {
            if (ModelState.IsValid) {
                var toUpdate = context.Services.Where(v => v.ServiceId == sid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.ServiceTypeId = value.ServiceTypeId;
                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a service.
        /// </summary>
        /// <param name="sid">ServiceId</param>
        [HttpDelete("{sid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Service (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Service>> DeleteService([FromRoute] int sid) {
            var toDelete = context.Services.Where(v => v.ServiceId == sid);
            context.Services.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
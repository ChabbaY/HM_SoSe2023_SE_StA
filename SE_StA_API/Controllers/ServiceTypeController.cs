using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for service types.
    /// </summary>
    [Route("api/serviceTypes")]
    [ApiController]
    public class ServiceTypeController : ControllerBase {
		 private ApplicationContext context;
        public ServiceTypeController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all service types.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "ServiceTypes (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ServiceType[]> GetAllServiceTypes() {
            return Ok(context.ServiceTypes.ToArray());
        }

        /// <summary>
        /// Returns the service type with a given id.
        /// </summary>
        /// <param name="stid">ServiceTypeID</param>
        [HttpGet("{stid}")]
        [SwaggerOperation(Tags = new[] { "Service Type (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ServiceType> GetServiceType ([FromRoute] int stid) {
            var value = context.ServiceTypes.Where(v => v.ServiceTypeId == stid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a service type.
        /// </summary>
        /// <param name="value">new service type</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Service Type (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ServiceType>> AddServiceType([FromBody] ServiceType value) {
            if (ModelState.IsValid) {
                //test if service type already exists
                if (context.ServiceTypes.Where(v => v.ServiceTypeId == value.ServiceTypeId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Service Type already exists");
                    return Conflict(ModelState); //service type with id already exists, we return a conflict
                }

                context.ServiceTypes.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the service type
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of service type
        }

        /// <summary>
        /// Updates a service type.
        /// </summary>
        /// <param name="stid">ServiceTypeId</param>
        /// <param name="value">new ServiceType</param>
        [HttpPut("{stid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Service Type (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceType>> UpdateServiceType([FromRoute] int stid, [FromBody] ServiceType value) {
            if (ModelState.IsValid) {
                var toUpdate = context.ServiceTypes.Where(v => v.ServiceTypeId == stid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.Name = value.Name;
                    toUpdate.DefaultPrice = value.DefaultPrice;
                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a service type.
        /// </summary>
        /// <param name="stid">ServiceTypeId</param>
        [HttpDelete("{stid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Service Type (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ServiceType>> DeleteServiceType([FromRoute] int stid) {
            var toDelete = context.ServiceTypes.Where(v => v.ServiceTypeId == stid);
            context.ServiceTypes.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
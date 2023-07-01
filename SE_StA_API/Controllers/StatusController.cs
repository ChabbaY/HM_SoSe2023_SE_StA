using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for statuses.
    /// </summary>
    [Route("api/statuses")]
    [ApiController]
    public class StatusController : ControllerBase {
        private ApplicationContext context;
        public StatusController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all statuses.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Statuses (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Status[]> GetAllStatuses() {
            return Ok(context.Statuses.ToArray());
        }

        /// <summary>
        /// Returns the status with a given id.
        /// </summary>
        /// <param name="sid">StatusID</param>
        [HttpGet("{sid}")]
        [SwaggerOperation(Tags = new[] { "Status (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Status> GetStatus ([FromRoute] int sid) {
            var value = context.Statuses.Where(v => v.StatusId == sid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a status.
        /// </summary>
        /// <param name="value">new status</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Status (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Status>> AddStatus([FromBody] Status value) {
            if (ModelState.IsValid) {
                //test if status already exists
                if (context.Statuses.Where(v => v.StatusId == value.StatusId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Status already exists");
                    return Conflict(ModelState); //status with id already exists, we return a conflict
                }

                context.Statuses.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the status
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of status
        }

        /// <summary>
        /// Updates a status.
        /// </summary>
        /// <param name="sid">StatusId</param>
        /// <param name="value">new Status</param>
        [HttpPut("{sid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Status (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Status>> UpdateStatus([FromRoute] int sid, [FromBody] Status value) {
            if (ModelState.IsValid) {
                var toUpdate = context.Statuses.Where(v => v.StatusId == sid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.Name = value.Name;
                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a status.
        /// </summary>
        /// <param name="sid">StatusId</param>
        [HttpDelete("{sid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Status (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Status>> DeleteStatus([FromRoute] int sid) {
            var toDelete = context.Statuses.Where(v => v.StatusId == sid);
            context.Statuses.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
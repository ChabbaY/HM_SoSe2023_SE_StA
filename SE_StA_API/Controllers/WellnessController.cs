using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for wellnesses.
    /// </summary>
    [Route("api/wellnesses")]
    [ApiController]
    public class WellnessController : ControllerBase {
    private ApplicationContext context;
        public WellnessController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all wellnesses.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Wellnesses (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Wellness[]> GetAllWellnesses() {
            return Ok(context.Wellnesses.ToArray());
        }

        /// <summary>
        /// Returns the wellness with a given id.
        /// </summary>
        /// <param name="wid">WellnessID</param>
        [HttpGet("{wid}")]
        [SwaggerOperation(Tags = new[] { "Wellness (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Wellness> GetWellness ([FromRoute] int wid) {
            var value = context.Wellnesses.Where(v => v.WellnessId == wid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a wellness.
        /// </summary>
        /// <param name="value">new wellness</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Wellness (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Wellness>> AddWellness([FromBody] Wellness value) {
            if (ModelState.IsValid) {
                //test if wellness already exists
                if (context.Wellnesses.Where(v => v.WellnessId == value.WellnessId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Wellness already exists");
                    return Conflict(ModelState); //wellness with id already exists, we return a conflict
                }

                context.Wellnesses.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the wellness
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of wellness
        }

        /// <summary>
        /// Updates a wellness.
        /// </summary>
        /// <param name="wid">WellnessId</param>
        /// <param name="value">new Wellness</param>
        [HttpPut("{wid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Wellness (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Wellness>> UpdateWellness([FromRoute] int wid, [FromBody] Wellness value) {
            if (ModelState.IsValid) {
                var toUpdate = context.Wellnesses.Where(v => v.WellnessId == wid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.Name = value.Name;
                    toUpdate.Duration = value.Duration;
                    toUpdate.ServiceId = value.ServiceId;
                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a wellness.
        /// </summary>
        /// <param name="wid">WellnessId</param>
        [HttpDelete("{wid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Wellness (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Wellness>> DeleteWellness([FromRoute] int wid) {
            var toDelete = context.Wellnesses.Where(v => v.WellnessId == wid);
            context.Wellnesses.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
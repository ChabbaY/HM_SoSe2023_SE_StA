using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for cashes.
    /// </summary>
    [Route("api/cashes")]
    [ApiController]
    public class CashController : ControllerBase {
		private ApplicationContext context;
        public CashController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all cashes.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Cash (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Cash[]> GetAllCashes() {
            return Ok(context.Cashes.ToArray());
        }

        /// <summary>
        /// Returns the cashes with a given id.
        /// </summary>
        /// <param name="cid">CashID</param>
        [HttpGet("{cid}")]
        [SwaggerOperation(Tags = new[] { "Cash (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Cash> GetCash ([FromRoute] int cid) {
            var value = context.Cashes.Where(v => v.CashId == cid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a cash.
        /// </summary>
        /// <param name="value">new Cash</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Cash (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Cash>> AddCash([FromBody] Cash value) {
            if (ModelState.IsValid) {
                //test if cash already exists
                if (context.Cashes.Where(v => v.CashId == value.CashId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Cash already exists");
                    return Conflict(ModelState); //cash with id already exists, we return a conflict
                }

                context.Cashes.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the cash
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of cash
        }

        /// <summary>
        /// Updates a cash.
        /// </summary>
        /// <param name="cid">CashId</param>
        /// <param name="value">new Cash</param>
        [HttpPut("{cid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Cash (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cash>> UpdateCash([FromRoute] int cid, [FromBody] Cash value) {
            if (ModelState.IsValid) {
                var toUpdate = context.Cashes.Where(v => v.CashId == cid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.PaymentMethod = value.PaymentMethod;
                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Delete a cash.
        /// </summary>
        /// <param name="cid">CashId</param>
        [HttpDelete("{cid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Cash (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Cash>> DeleteCash([FromRoute] int cid) {
            var toDelete = context.Cashes.Where(v => v.CashId == cid);
            context.Cashes.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
		
    }
}
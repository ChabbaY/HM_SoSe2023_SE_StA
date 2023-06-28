using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for credit cards.
    /// </summary>
    [Route("api/creditCards")]
    [ApiController]
    public class CreditCardController : ControllerBase {
		
		private ApplicationContext context;
        public CreditCardController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all credit cards.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Credit Card (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CreditCard[]> GetAllCreditCards() {
            return Ok(context.CreditCards.ToArray());
        }

        /// <summary>
        /// Returns the credit card with a given id.
        /// </summary>
        /// <param name="ccid">CreditCardID</param>
        [HttpGet("{ccid}")]
        [SwaggerOperation(Tags = new[] { "Credit Card (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CreditCard> GetCreditCard ([FromRoute] int ccid) {
            var value = context.CreditCards.Where(v => v.CreditCardId == ccid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a credit card.
        /// </summary>
        /// <param name="value">new credit card</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Credit Card (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreditCard>> AddCreditCard([FromBody] CreditCard value) {
            if (ModelState.IsValid) {
                //test if credit card already exists
                if (context.CreditCards.Where(v => v.CreditCardId == value.CreditCardId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Credit Card already exists");
                    return Conflict(ModelState); //credit card with id already exists, we return a conflict
                }

                context.CreditCards.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the credit card
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of credit card
        }

        /// <summary>
        /// Updates a credit card.
        /// </summary>
        /// <param name="ccid">CreditCardId</param>
        /// <param name="value">new CreditCard</param>
        [HttpPut("{ccid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Credit Card (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CreditCard>> UpdateCreditCard([FromRoute] int ccid, [FromBody] CreditCard value) {
            if (ModelState.IsValid) {
                var toUpdate = context.CreditCards.Where(v => v.CreditCardId == ccid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.CardNumber = value.CardNumber;
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
        /// Deletes a credit card.
        /// </summary>
        /// <param name="ccid">CreditCardId</param>
        [HttpDelete("{ccid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Credit Card (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreditCard>> DeleteCreditCard([FromRoute] int ccid) {
            var toDelete = context.CreditCards.Where(v => v.CreditCardId == ccid);
            context.CreditCards.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
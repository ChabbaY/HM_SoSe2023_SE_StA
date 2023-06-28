using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for payment methods.
    /// </summary>
    [Route("api/paymentMethods")]
    [ApiController]
    public class PaymentMethodController : ControllerBase {
       	private ApplicationContext context;
        public PaymentMethodController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all Payment Methods.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Payment Methods (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<PaymentMethod[]> GetAllPaymentMethods() {
            return Ok(context.PaymentMethods.ToArray());
        }

        /// <summary>
        /// Returns the payment method with a given id.
        /// </summary>
        /// <param name="pmid">PaymentMethodID</param>
        [HttpGet("{pmid}")]
        [SwaggerOperation(Tags = new[] { "Payment Method (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PaymentMethod> GetPaymentMethod ([FromRoute] int pmid) {
            var value = context.PaymentMethods.Where(v => v.PaymentMethodId == pmid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a payment method.
        /// </summary>
        /// <param name="value">new payment method</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Payment Method (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<PaymentMethod>> AddPaymentMethod([FromBody] PaymentMethod value) {
            if (ModelState.IsValid) {
                //test if paymentvmethod already exists
                if (context.PaymentMethods.Where(v => v.PaymentMethodId == value.PaymentMethodId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Payment Method already exists");
                    return Conflict(ModelState); //payment method with id already exists, we return a conflict
                }

                context.PaymentMethods.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the payment method
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of payment method
        }

        /// <summary>
        /// Updates a payment method.
        /// </summary>
        /// <param name="pmid">PaymentMethodId</param>
        /// <param name="value">new Payment Method</param>
        [HttpPut("{pmid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Payment Method (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentMethod>> UpdateHotel([FromRoute] int pmid, [FromBody] PaymentMethod value) {
            if (ModelState.IsValid) {
                var toUpdate = context.PaymentMethods.Where(v => v.PaymentMethodId == pmid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.CustomerId = value.CustomerId;
                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a payment method.
        /// </summary>
        /// <param name="pmid">PaymentMethodId</param>
        [HttpDelete("{pmid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Payment Method (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<PaymentMethod>> DeletePaymentMethod([FromRoute] int pmid) {
            var toDelete = context.PaymentMethods.Where(v => v.PaymentMethodId == pmid);
            context.PaymentMethods.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        } 
		
    }
}
using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for invoices.
    /// </summary>
    [Route("api/invoices")]
    [ApiController]
    public class InvoiceController : ControllerBase {
		private ApplicationContext context;
        public InvoiceController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all invoices.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Invoices (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Invoice[]> GetAllInvoices() {
            return Ok(context.Invoices.ToArray());
        }

        /// <summary>
        /// Returns the invoice with a given id.
        /// </summary>
        /// <param name="iid">InvoiceID</param>
        [HttpGet("{iid}")]
        [SwaggerOperation(Tags = new[] { "Invoice (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Invoice> GetInvoice ([FromRoute] int iid) {
            var value = context.Invoices.Where(v => v.InvoiceId == iid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds an invoice.
        /// </summary>
        /// <param name="value">new invoice</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Invoice (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Invoice>> AddInvoice([FromBody] Invoice value) {
            if (ModelState.IsValid) {
                //test if invoice already exists
                if (context.Invoices.Where(v => v.InvoiceId == value.InvoiceId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Invoice already exists");
                    return Conflict(ModelState); //invoice with id already exists, we return a conflict
                }

                context.Invoices.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the invoice
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of invoice
        }

        /// <summary>
        /// Updates an invoice.
        /// </summary>
        /// <param name="iid">InvoiceId</param>
        /// <param name="value">new Invoice</param>
        [HttpPut("{iid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Invoice (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Invoice>> UpdateHotel([FromRoute] int iid, [FromBody] Invoice value) {
            if (ModelState.IsValid) {
                var toUpdate = context.Invoices.Where(v => v.InvoiceId == iid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.Number = value.Number;
                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes an invoice.
        /// </summary>
        /// <param name="iid">InvoiceId</param>
        [HttpDelete("{iid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Invoice (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Invoice>> DeleteInvoice([FromRoute] int iid) {
            var toDelete = context.Invoices.Where(v => v.InvoiceId == iid);
            context.Invoices.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
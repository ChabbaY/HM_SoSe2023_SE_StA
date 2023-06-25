using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for bank accounts.
    /// </summary>
    [Route("api/bankAccounts")]
    [ApiController]
    public class BankAccountController : ControllerBase {
		private ApplicationContext context;
        public BankAccountController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all bank accounts.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "BankAccount (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<BankAccount[]> GetAllBankAccounts() {
            return Ok(context.BankAccounts.ToArray());
        }

        /// <summary>
        /// Returns the bank account with a given id.
        /// </summary>
        /// <param name="baid">BankAccountID</param>
        [HttpGet("{baid}")]
        [SwaggerOperation(Tags = new[] { "BankAccount (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BankAccount> GetBankAccount([FromRoute] int baid) {
            var value = context.BankAccounts.Where(v => v.BankAccountId == baid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a bank account.
        /// </summary>
        /// <param name="value">new Adress</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Bank account (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<BankAccount>> AddBankAccount([FromBody] BankAccount value) {
            if (ModelState.IsValid) {
                //test if bank account already exists
                if (context.BankAccounts.Where(v => v.BankAccountId == value.BankAccountId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Bank account already exists");
                    return Conflict(ModelState); //bank account with id already exists, we return a conflict
                }

                context.BankAccounts.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the bank account
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of bank account
        }

        /// <summary>
        /// Updates a bank account.
        /// </summary>
        /// <param name="baid">BankAccountId</param>
        /// <param name="value">new BankAccount</param>
        [HttpPut("{baid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Bank account (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BankAccount>> UpdateBankAccount([FromRoute] int baid, [FromBody] BankAccount value) {
            if (ModelState.IsValid) {
                var toUpdate = context.BankAccounts.Where(v => v.BankAccountId == baid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.Iban = value.Iban;
                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Delete a bank account.
        /// </summary>
        /// <param name="baid">BankACcountId</param>
        [HttpDelete("{baid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Bank account (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<BankAccount>> DeleteBankAccount([FromRoute] int baid) {
            var toDelete = context.BankAccounts.Where(v => v.BankAccountId == baid);
            context.BankAccounts.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
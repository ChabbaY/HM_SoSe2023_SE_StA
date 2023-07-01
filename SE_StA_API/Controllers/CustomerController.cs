using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for customers.
    /// </summary>
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase {
			private ApplicationContext context;
        public CustomerController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all customers.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Customer (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Customer[]> GetAllCustomers() {
            return Ok(context.Customers.ToArray());
        }

        /// <summary>
        /// Returns the customer with a given id.
        /// </summary>
        /// <param name="cid">CustomerID</param>
        [HttpGet("{cid}")]
        [SwaggerOperation(Tags = new[] { "Customer (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Customer> GetCustomer ([FromRoute] int cid) {
            var value = context.Customers.Where(v => v.CustomerId == cid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a customer.
        /// </summary>
        /// <param name="value">new customer</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Customer (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Customer>> AddCustomer([FromBody] Customer value) {
            if (ModelState.IsValid) {
                //test if customer already exists
                if (context.Customers.Where(v => v.CustomerId == value.CustomerId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Customer already exists");
                    return Conflict(ModelState); //customer with id already exists, we return a conflict
                }

                context.Customers.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the customer
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of customer
        }

        /// <summary>
        /// Updates a customer.
        /// </summary>
        /// <param name="cid">CustomerId</param>
        /// <param name="value">new Customer</param>
        [HttpPut("{cid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Customer (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Customer>> UpdateCustomer([FromRoute] int cid, [FromBody] Customer value) {
            if (ModelState.IsValid) {
                var toUpdate = context.Customers.Where(v => v.CustomerId == cid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.FirstName = value.FirstName;
                    toUpdate.LastName = value.LastName;
                    toUpdate.DateOfBirth = value.DateOfBirth;
                    toUpdate.Number = value.Number;
                    toUpdate.ContactId = value.ContactId;
                    toUpdate.UserId = value.UserId;
                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a customer.
        /// </summary>
        /// <param name="cid">CustomerId</param>
        [HttpDelete("{cid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Customer (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Customer>> DeleteCustomer([FromRoute] int cid) {
            var toDelete = context.Customers.Where(v => v.CustomerId == cid);
            context.Customers.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
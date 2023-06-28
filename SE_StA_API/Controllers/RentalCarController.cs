using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for rental cars.
    /// </summary>
    [Route("api/rentalCars")]
    [ApiController]
    public class RentalCarController : ControllerBase {
	private ApplicationContext context;
        public RentalCarController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all rental cars.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Rental Cars (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<RentalCar[]> GetAllRentalCars() {
            return Ok(context.RentalCars.ToArray());
        }

        /// <summary>
        /// Returns the rental car with a given id.
        /// </summary>
        /// <param name="rcid">RentalCarID</param>
        [HttpGet("{rcid}")]
        [SwaggerOperation(Tags = new[] { "Rental Car (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RentalCar> GetRentalCar ([FromRoute] int rcid) {
            var value = context.RentalCars.Where(v => v.RentalCarId == rcid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a rental car.
        /// </summary>
        /// <param name="value">new rental car</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Rental Car (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<RentalCar>> AddRentalCar([FromBody] RentalCar value) {
            if (ModelState.IsValid) {
                //test if rental car already exists
                if (context.RentalCars.Where(v => v.RentalCarId == value.RentalCarId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Rental Car already exists");
                    return Conflict(ModelState); //rental car with id already exists, we return a conflict
                }

                context.RentalCars.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the rental car
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of rental car
        }

        /// <summary>
        /// Updates a rental car.
        /// </summary>
        /// <param name="rcid">RentalCarId</param>
        /// <param name="value">new RentalCar</param>
        [HttpPut("{rcid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Rental Car (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RentalCar>> UpdateRentalCar([FromRoute] int rcid, [FromBody] RentalCar value) {
            if (ModelState.IsValid) {
                var toUpdate = context.RentalCars.Where(v => v.RentalCarId == rcid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.CarModel = value.CarModel;
                    toUpdate.Seats = value.Seats;
                    toUpdate.ServiceId  = value.ServiceId;
                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a rental car.
        /// </summary>
        /// <param name="rcid">RentalCarId</param>
        [HttpDelete("{rcid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Rental Car (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<RentalCar>> DeleteRentalCar([FromRoute] int rcid) {
            var toDelete = context.RentalCars.Where(v => v.RentalCarId == rcid);
            context.RentalCars.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }	
    }
}
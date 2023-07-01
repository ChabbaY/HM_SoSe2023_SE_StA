using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for hotels.
    /// </summary>
    [Route("api/hotels")]
    [ApiController]
    public class HotelController : ControllerBase {
	    private ApplicationContext context;
        public HotelController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all hotels.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Hotels (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Hotel[]> GetAllHotels() {
            return Ok(context.Hotels.ToArray());
        }

        /// <summary>
        /// Returns the hotel with a given id.
        /// </summary>
        /// <param name="hid">HotelID</param>
        [HttpGet("{hid}")]
        [SwaggerOperation(Tags = new[] { "Hotel (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Hotel> GetHotel ([FromRoute] int hid) {
            var value = context.Hotels.Where(v => v.HotelId == hid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a hotel.
        /// </summary>
        /// <param name="value">new hotel</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Hotel (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Hotel>> AddHotel([FromBody] Hotel value) {
            if (ModelState.IsValid) {
                //test if hotel already exists
                if (context.Hotels.Where(v => v.HotelId == value.HotelId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Hotel already exists");
                    return Conflict(ModelState); //hotel with id already exists, we return a conflict
                }

                context.Hotels.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the hotel
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of hotel
        }

        /// <summary>
        /// Updates a hotel.
        /// </summary>
        /// <param name="hid">HotelId</param>
        /// <param name="value">new Hotel</param>
        [HttpPut("{hid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Hotel (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Hotel>> UpdateHotel([FromRoute] int hid, [FromBody] Hotel value) {
            if (ModelState.IsValid) {
                var toUpdate = context.Hotels.Where(v => v.HotelId == hid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.Name = value.Name;
                    toUpdate.Stars = value.Stars;
                    toUpdate.ContactId  = value.ContactId;
                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a hotel.
        /// </summary>
        /// <param name="hid">HotelId</param>
        [HttpDelete("{hid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Hotel (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Hotel>> DeleteHotel([FromRoute] int hid) {
            var toDelete = context.Hotels.Where(v => v.HotelId == hid);
            context.Hotels.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
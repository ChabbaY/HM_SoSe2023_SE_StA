using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for room types.
    /// </summary>
    [Route("api/roomTypes")]
    [ApiController]
    public class RoomTypeController : ControllerBase {
		
	private ApplicationContext context;
        public RoomTypeController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all room types.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Room Types (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<RoomType[]> GetAllRoomTypes() {
            return Ok(context.RoomTypes.ToArray());
        }

        /// <summary>
        /// Returns the room type with a given id.
        /// </summary>
        /// <param name="rtid">RoomTypeID</param>
        [HttpGet("{rtid}")]
        [SwaggerOperation(Tags = new[] { "Room Type (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RoomType> GetRoomType ([FromRoute] int rtid) {
            var value = context.RoomTypes.Where(v => v.RoomTypeId == rtid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a room type.
        /// </summary>
        /// <param name="value">new room type</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Room Type (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<RoomType>> AddRoomType([FromBody] RoomType value) {
            if (ModelState.IsValid) {
                //test if room type already exists
                if (context.RoomTypes.Where(v => v.RoomTypeId == value.RoomTypeId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Room Type already exists");
                    return Conflict(ModelState); //room type with id already exists, we return a conflict
                }

                context.RoomTypes.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the room types
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of room types
        }

        /// <summary>
        /// Updates a room type.
        /// </summary>
        /// <param name="rtid">RoomTypeId</param>
        /// <param name="value">new RoomType</param>
        [HttpPut("{rtid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Room Type (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomType>> UpdateRoomType([FromRoute] int rtid, [FromBody] RoomType value) {
            if (ModelState.IsValid) {
                var toUpdate = context.RoomTypes.Where(v => v.RoomTypeId == rtid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.Name = value.Name;
                    toUpdate.DefaultPrice = value.DefaultPrice;
                    toUpdate.PersonsCount  = value.PersonsCount;
                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a room type.
        /// </summary>
        /// <param name="rtid">RoomTypeId</param>
        [HttpDelete("{rtid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Room Type (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<RoomType>> DeleteRoomType([FromRoute] int rtid) {
            var toDelete = context.RoomTypes.Where(v => v.RoomTypeId == rtid);
            context.RoomTypes.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }	
    }
}
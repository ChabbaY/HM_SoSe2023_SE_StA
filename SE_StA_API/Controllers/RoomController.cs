using SE_StA_API.DataObject;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace SE_StA_API.Controllers {
    /// <summary>
    /// This endpoint manages all operations for rooms.
    /// </summary>
    [Route("api/rooms")]
    [ApiController]
    public class RoomController : ControllerBase {
		
	private ApplicationContext context;
        public RoomController(ApplicationContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns all rooms.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Rooms (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Room[]> GetAllRooms() {
            return Ok(context.Rooms.ToArray());
        }

        /// <summary>
        /// Returns the room with a given id.
        /// </summary>
        /// <param name="rid">RoomID</param>
        [HttpGet("{rid}")]
        [SwaggerOperation(Tags = new[] { "Room (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Room> GetRoom ([FromRoute] int rid) {
            var value = context.Rooms.Where(v => v.RoomId == rid).FirstOrDefault();
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        /// <summary>
        /// Adds a room.
        /// </summary>
        /// <param name="value">new room</param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Room (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Room>> AddRoom([FromBody] Room value) {
            if (ModelState.IsValid) {
                //test if room already exists
                if (context.Rooms.Where(v => v.RoomId == value.RoomId).FirstOrDefault() != null) {
                    ModelState.AddModelError("validationError", "Room already exists");
                    return Conflict(ModelState); //room with id already exists, we return a conflict
                }

                context.Rooms.Add(value);
                await context.SaveChangesAsync();

                return Ok(value); //we return the room
            }
            return BadRequest(ModelState); //Model is not valid -> Validation Annotation of room
        }

        /// <summary>
        /// Updates a room.
        /// </summary>
        /// <param name="rid">RoomId</param>
        /// <param name="value">new Room</param>
        [HttpPut("{rid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Room (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Room>> UpdateRoom([FromRoute] int rid, [FromBody] Room value) {
            if (ModelState.IsValid) {
                var toUpdate = context.Rooms.Where(v => v.RoomId == rid).FirstOrDefault();
                if (toUpdate != null) {
                    toUpdate.RoomNumber = value.RoomNumber;
                    toUpdate.HotelId = value.HotelId;
                    toUpdate.RoomTypeId  = value.RoomTypeId;
                    
                    await context.SaveChangesAsync();

                    return Ok(value);
                } else {
                    return NotFound(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a room.
        /// </summary>
        /// <param name="rid">RoomId</param>
        [HttpDelete("{rid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Room (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Room>> DeleteRoom([FromRoute] int rid) {
            var toDelete = context.Rooms.Where(v => v.RoomId == rid);
            context.Rooms.RemoveRange(toDelete);

            await context.SaveChangesAsync();

            return Ok();
        }	
    }
}
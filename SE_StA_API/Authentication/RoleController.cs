using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace SE_StA_API.Authentication {
    [Route("auth/roles")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin")]
    public class RoleController : ControllerBase {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager) {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        //#############################
        // Roles
        //#############################

        /// <summary>
        /// Returns all roles.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Authentication - Roles (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllRoles() {
            return Ok(_roleManager.Roles.ToList());
        }

        /// <summary>
        /// Returns the role with a given name.
        /// </summary>
        /// <param name="roleName">roleName</param>
        [HttpGet("{roleName}")]
        [SwaggerOperation(Tags = new[] { "Authentication - Roles (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRole([FromRoute] string roleName) {
            var roleResult = await _roleManager.FindByNameAsync(roleName);
            if (roleResult != null) {
                return Ok(roleResult);
            } else {
                return NotFound();
            }
        }

        /// <summary>
        /// Adds a role.
        /// </summary>
        /// <param name="roleName">roleName</param>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Authentication - Roles (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRole([FromBody] string roleName) {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists) {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (roleResult.Succeeded) {
                    return Ok(new { result = $"Role {roleName} added successfully" });
                } else {
                    ModelState.AddModelError("Creation failed", "Role couldn't be created");
                    return BadRequest(ModelState);
                }
            }
            ModelState.AddModelError("Creation failed", "Role already exists");
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a role.
        /// </summary>
        /// <param name="roleName">roleName</param>
        [HttpDelete("{roleName}")]
        [SwaggerOperation(Tags = new[] { "Authentication - Roles (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRole([FromRoute] string roleName) {
            IList<IdentityUser> lst = await _userManager.GetUsersInRoleAsync(roleName);
            if (lst.Count > 0) {
                ModelState.AddModelError("Deleting failed", $"Role {roleName} is still referenced and thus cannot be deleted");
                return Conflict(ModelState);
            }

            var role = await _roleManager.FindByNameAsync(roleName);

            if (role != null) {
                return Ok(await _roleManager.DeleteAsync(role));
            } else {
                ModelState.AddModelError("Deleting failed", $"Role {roleName} does not exist");
                return NotFound(ModelState);
            }
        }

        //#############################
        // Users
        //#############################

        /// <summary>
        /// Returns all users, that are assigned to a specific role.
        /// </summary>
        /// <param name="roleName">roleName</param>
        [HttpGet("{roleName}/users")]
        [SwaggerOperation(Tags = new[] { "Authentication - User Roles (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAssignedUsers([FromRoute] string roleName) {
            return Ok(await _userManager.GetUsersInRoleAsync(roleName));
        }

        /// <summary>
        /// Adds an user to a specific role.
        /// </summary>
        /// <param name="roleName">roleName</param>
        /// <param name="email">the user's email</param>
        [HttpPost("{roleName}/users")]
        [SwaggerOperation(Tags = new[] { "Authentication - User Roles (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUserToRole([FromRoute] string roleName, [FromBody] string email) {
            var user = await _userManager.FindByEmailAsync(email);
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if ((user != null) && roleExists) {
                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (result.Succeeded) {
                    return Ok(new {result = $"User {user.Email} added to the {roleName} role"});
                } else {
                    ModelState.AddModelError("Assignment failed", $"Role {roleName} couldn't be assigned to {user.Email}");
                    return BadRequest(ModelState);
                }
            }
            ModelState.AddModelError("Assignment failed", "Unable to find user or role");
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Removes an user from a specific role.
        /// </summary>
        /// <param name="roleName">roleName</param>
        /// <param name="email">the user's email</param>
        [HttpDelete("{roleName}/users")]
        [SwaggerOperation(Tags = new[] { "Authentication - User Roles (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveUserFromRole([FromRoute] string roleName, [FromBody] string email) {
            var user = await _userManager.FindByEmailAsync(email);
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if ((user != null) && roleExists) {
                var result = await _userManager.RemoveFromRoleAsync(user, roleName);
                if (result.Succeeded) {
                    return Ok(new { result = $"User {user.Email} removed from the {roleName} role" });
                } else {
                    ModelState.AddModelError("Removal failed", $"Role {roleName} couldn't be removed from {user.Email}");
                    return BadRequest(ModelState);
                }
            }
            ModelState.AddModelError("Removal failed", "Unable to find user or role");
            return BadRequest(ModelState);
        }

        //#############################
        // Users
        //#############################

        /// <summary>
        /// Returns all roles with assigned users.
        /// </summary>
        [HttpGet("overview")]
        [SwaggerOperation(Tags = new[] { "Authentication - User Roles Overview (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRolesOverview() {
            List<object> result = new();
            var roles = _roleManager.Roles.ToList();
            foreach(var role in roles) {
                var users = await _userManager.GetUsersInRoleAsync(role.Name);
                result.Add(new {
                    role,
                    users
                });
            }
            return Ok(result);
        }
    }
}
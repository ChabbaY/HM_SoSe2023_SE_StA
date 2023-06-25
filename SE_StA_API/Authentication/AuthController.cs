using SE_StA_API.Authentication.TwoFactor;
using SE_StA_API.Email;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace SE_StA_API.Authentication {
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationContext _context;
        private readonly TokenService _tokenService;
        private readonly IEmailSender _emailSender;
        public AuthController(UserManager<IdentityUser> userManager, ApplicationContext context, TokenService tokenService, IEmailSender emailSender) {
            _userManager = userManager;
            _context = context;
            _tokenService = tokenService;
            _emailSender = emailSender;
        }

        /// <summary>
        /// Creates an user account with the given data.
        /// </summary>
        /// <param name="request">Registration Request Data</param>
        /// <returns></returns>
        [HttpPost("register")]
        [SwaggerOperation(Tags = new[] { "Authentication (Public)" })]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var user = new IdentityUser {
                UserName = request.Username,
                Email = request.Email
            };
            var result = await _userManager.CreateAsync(
                user,
                request.Password
            );

            if (result.Succeeded) {
                //email verification
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new {
                    token,
                    email = user.Email
                }, Request.Scheme);
                var message = new Message(
                    new IdentityUser[] { user },
                    "Account Verification",
                    "Please verify your email.<br />" +
                    $"Your Token is: {token}<br />" +
                    "and expires in about 2 hours.<br />" +
                    $"You can just follow this link: {confirmationLink}",
                    null);
                await _emailSender.SendEmailAsync(message);

                request.Password = "";
                return CreatedAtAction(nameof(Register), new { email = request.Email }, request);
            }

            foreach (var error in result.Errors) {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Returns an access token when correct login information is provided.
        /// </summary>
        /// <param name="request">Authentication Request Data</param>
        /// <returns></returns>
        [HttpPost("login")]
        [SwaggerOperation(Tags = new[] { "Authentication (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var managedUser = await _userManager.FindByEmailAsync(request.Email);
            if (managedUser == null) {
                return BadRequest("Bad credentails");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);
            if (!isPasswordValid) {
                return BadRequest("Bad credentails");
            }

            var userInDb = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (userInDb is null) {
                return Unauthorized();
            }

            if (!userInDb.EmailConfirmed) {
                return Unauthorized("Email not verified");
            }

            if (userInDb.TwoFactorEnabled) {
                if (!TwoFactorAuthController.IsValid(userInDb, request.FactorCode)) {
                    return Unauthorized("2FA check failed");
                }
            }

            var accessToken = await _tokenService.CreateToken(userInDb);
            await _context.SaveChangesAsync();
            return Ok(new AuthResponse {
                Username = userInDb.UserName,
                Email = userInDb.Email,
                Token = accessToken
            });
        }

        /// <summary>
        /// Verifies an emil address.
        /// </summary>
        /// <param name="token">Email Verification Token</param>
        /// <param name="email">Email Address</param>
        [HttpGet("validate")]
        [SwaggerOperation(Tags = new[] { "Authentication (Public)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmEmail(string token, string email) {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) {
                ModelState.AddModelError("Confirmation failed", "User not found");
                return BadRequest(ModelState);
            } else {
                var confirmed = await _userManager.IsEmailConfirmedAsync(user);
                if (confirmed) {
                    return Ok("Email bereits bestätigt!");
                }
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded) {
                    result = await _userManager.AddToRoleAsync(user, "User");
                    if (result.Succeeded) {
                        return Ok("Email erfolgreich bestätigt!");
                    } else {
                        ModelState.AddModelError("Could not add to role User", "Make sure the role User exists");
                        return BadRequest(ModelState);
                    }
                } else {
                    ModelState.AddModelError("Confirmation failed", "Failed to confirm token");
                    return BadRequest(ModelState);
                }
            }
        }

        //######################
        // Admin Tasks
        //######################

        /// <summary>
        /// Returns all registered users.
        /// </summary>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Authentication (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<IdentityUser>> GetAllUsers() {
            return Ok(_userManager.Users.ToList());
        }

        /// <summary>
        /// Deletes an user.
        /// </summary>
        /// <param name="email">Email Address</param>
        [HttpDelete("{email}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        [SwaggerOperation(Tags = new[] { "Authentication (Admin)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser([FromRoute] string email) {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null) {
                return Ok(await _userManager.DeleteAsync(user));
            } else {
                ModelState.AddModelError("Deleting failed", "Could not delete user");
                return BadRequest(ModelState);
            }
        }
    }
}
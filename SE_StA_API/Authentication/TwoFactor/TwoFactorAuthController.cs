using Google.Authenticator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace SE_StA_API.Authentication.TwoFactor {
    [Route("auth/2fa")]
    [ApiController]
    public class TwoFactorAuthController : ControllerBase {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TwoFactorAuthenticator _twoFactor;
        public TwoFactorAuthController(UserManager<IdentityUser> userManager) {
            _userManager = userManager;
            _twoFactor = new TwoFactorAuthenticator();
        }

        /// <summary>
        /// Returns the code to setup 2FA, also as QR-Code.
        /// </summary>
        [HttpGet("enable")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Tags = new[] { "Two Factor Authentication (User)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Enable() {
            //retrieve current user
            IdentityUser user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            var setupInfo = _twoFactor.GenerateSetupCode("ChabbaY", user.Email, TwoFactorKey(user), false, 3);

            return Ok(new {
                key = setupInfo.ManualEntryKey,
                qr = setupInfo.QrCodeSetupImageUrl
            });
        }

        /// <summary>
        /// Activates 2FA for the user if code is valid.
        /// </summary>
        /// <param name="activationCode">Code from the 2FA client</param>
        [HttpPost("enable")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Tags = new[] { "Two Factor Authentication (User)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Enable(string activationCode) {
            //retrieve current user
            IdentityUser user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            if (!IsValid(user, activationCode)) {
                ModelState.AddModelError("Validation failed", "Validation with code failed");
                return BadRequest(ModelState);
            }

            var result = await _userManager.SetTwoFactorEnabledAsync(user, true);
            if (result.Succeeded) {
                return Ok(result);
            } else {
                ModelState.AddModelError("Setup failed", "Enabling 2FA failed");
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Deactivates 2FA for the user if code is valid.
        /// </summary>
        /// <param name="code">Code from the 2FA client</param>
        [HttpPost("disable")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Tags = new[] { "Two Factor Authentication (User)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Disable(string code) {
            //retrieve current user
            IdentityUser user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            if (!IsValid(user, code)) {
                ModelState.AddModelError("Validation failed", "Validation with code failed");
                return BadRequest(ModelState);
            }

            var result = await _userManager.SetTwoFactorEnabledAsync(user, false);
            if (result.Succeeded) {
                return Ok(result);
            } else {
                ModelState.AddModelError("Setup failed", "Disabling 2FA failed");
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Returns wether or not 2FA is enabled (for the current user).
        /// </summary>
        [HttpGet("enabled")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Tags = new[] { "Two Factor Authentication (User)" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> IsEnabled() {
            //retrieve current user
            IdentityUser user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            return Ok(user!.TwoFactorEnabled);
        }

        public static bool IsValid(IdentityUser user, string code) {
            TwoFactorAuthenticator twoFactor = new TwoFactorAuthenticator();
            return twoFactor.ValidateTwoFactorPIN(TwoFactorKey(user), code);
        }

        private static string TwoFactorKey(IdentityUser user) {
            return $"gL-n_yK+prZbF+{user.Email}";
        }
    }
}
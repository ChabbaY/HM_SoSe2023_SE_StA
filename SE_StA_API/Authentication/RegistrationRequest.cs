using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.Authentication {
    public class RegistrationRequest {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
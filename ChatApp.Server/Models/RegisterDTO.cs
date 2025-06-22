using System.ComponentModel.DataAnnotations;

namespace ChatApp.Server.Models
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required, Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}



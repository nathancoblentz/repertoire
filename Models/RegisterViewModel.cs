// Models for the database context

using System.ComponentModel.DataAnnotations;

namespace CoblentzContext.Models
{
    // Model for the database context
    public class RegisterViewModel
    {
        // Username
        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(255)]
        public string UserName { get; set; } = string.Empty;

        // Password
        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; } = string.Empty;

        // Confirm Password
        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}

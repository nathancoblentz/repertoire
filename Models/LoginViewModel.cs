// Models for the database context

using System.ComponentModel.DataAnnotations;

namespace CoblentzContext.Models
{
    // Model for the database context
    public class LoginViewModel
    {
        // Username
        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(255)] 
        public string UserName { get; set; } = string.Empty;

        // Password
        [Required(ErrorMessage = "Please enter a password.")]
        [StringLength(255)]
        public string Password { get; set; } = string.Empty;

        // Return URL
        public string ReturnURL { get; set; } = string.Empty;

        // Remember me
        public bool RememberMe { get; set; }
    }
}

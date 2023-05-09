using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.WebAPI.Models
{
    public class UserRegistrationModel
    {
        [Required]
        [EmailAddress]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "The passwords must match")]
        [Required]
        public string? ConfirmPassword { get; set; }
    }
}

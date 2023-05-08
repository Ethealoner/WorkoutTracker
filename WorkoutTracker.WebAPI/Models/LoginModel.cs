using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.WebAPI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "User password is required")]
        public string? Password { get; set; }
    }
}



using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.WebAPI.Models
{

    public class AddWorkoutSessionModel
    {
        [Required(ErrorMessage = "User Id is required")]
        public string userId { get; set; }
        [Required(ErrorMessage = "Workout session date is required")]
        public DateTime createdAt { get; set; }
    }
}

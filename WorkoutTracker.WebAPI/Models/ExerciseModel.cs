using System.ComponentModel.DataAnnotations;
using WorkoutTracker.Core.Enums;

namespace WorkoutTracker.WebAPI.Models
{
    public class ExerciseModel
    {
        [Required(ErrorMessage = "Exercise name is required")]
        public string exerciseName { get; set; }

        public int exerciseId { get; set; }
        [Required(ErrorMessage = "workout session id is required")]
        public string workoutSessionId { get; set; }
        [Required(ErrorMessage = "Type of exercise is required")]
        public string typeOfExercise { get; set; }
        [Required(ErrorMessage = "repetitions is required")]
        public string repetitions { get; set; }

    }
}

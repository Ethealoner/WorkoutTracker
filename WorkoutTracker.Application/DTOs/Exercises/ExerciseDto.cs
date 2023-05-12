using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Application.DTOs.Exercises
{
    public class ExerciseDto
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

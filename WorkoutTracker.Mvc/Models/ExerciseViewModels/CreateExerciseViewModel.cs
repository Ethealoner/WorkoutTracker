using System.ComponentModel.DataAnnotations;
using WorkoutTracker.Core.Enums;
using WorkoutTracker.Core.Models;

namespace WorkoutTrackerMvc.Models.ExerciseViewModels
{
    public class CreateExerciseViewModel : IValidatableObject
    {
        public string workoutSessionId { get; set; }
        public int? exerciseId { get; set; }

        [Display(Name = "Exercise Name")]
        [Required(ErrorMessage = "Exercise Name must not be empty")]
        [StringLength(30, ErrorMessage = "Maximum number of characters is 30")]
        public string exerciseName { get; set; }

        public TypeOfExercise ExerciseType { get; set; }

        public ICollection<Set> Sets { get; set; }

        public CreateExerciseViewModel(string workoutSessionId)
        {
            this.workoutSessionId = workoutSessionId;
            
        }
        public CreateExerciseViewModel()
        {

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach (var set in Sets)
            {
                if (set == null)
                    continue;

                if (set.Repetitions <= 0)
                    yield return new ValidationResult("Repetition number must be greater than 0");
                if (set.Difficulty <= 0)
                    yield return new ValidationResult("Difficulty number must be greater than 0");
            }
        }
    }
}

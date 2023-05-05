using WorkoutTracker.Core.Enums;
using WorkoutTracker.Core.Models;

namespace WorkoutTrackerMvc.Models.ExerciseViewModels
{
    public class CreateExerciseViewModel
    {
        public string workoutSessionId { get; set; }
        public int? exerciseId { get; set; }
        public string exerciseName { get; set; }

        public TypeOfExercise ExerciseType { get; set; }

        public List<Set> sets { get; set; }
        public Set setToAdd { get; set; }

        public CreateExerciseViewModel(string workoutSessionId)
        {
            this.workoutSessionId = workoutSessionId;
            
        }
        public CreateExerciseViewModel()
        {
            sets = new List<Set>();
        }
    }
}

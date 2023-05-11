namespace WorkoutTracker.WebAPI.Models
{
    public class WorkoutSessionDetails
    {
        public string WorkoutSessionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ExerciseSummary> exerciseSummaries { get; set; }
    }

    public class ExerciseSummary
    {
        public string ExerciseName { get; set; }
        public int ExerciseId { get; set; }

        public ExerciseSummary(string exerciseName, int exerciseId)
        {
            ExerciseName = exerciseName;
            ExerciseId = exerciseId;
        }
    }
}

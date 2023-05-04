using WorkoutTracker.Core.Models;

namespace WorkoutTrackerMvc.Models.WorkoutSessionViewModels
{
    public class WorkoutSessionDetail
    {
        public DateTime date { get; set; }
        public string userId { get; set; }
        public string workoutSessionId { get; set; }
        public ICollection<Exercise> exercises { get; set; }
    }
}

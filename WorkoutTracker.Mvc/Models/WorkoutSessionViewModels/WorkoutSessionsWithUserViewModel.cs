using WorkoutTracker.Core.Models;

namespace WorkoutTrackerMvc.Models.WorkoutSessionViewModels
{
    public class WorkoutSessionsWithUserViewModel
    {
        public string UserId { get; set; }
        public IEnumerable<WorkoutSession> workoutSessions { get; set; }
    }
}

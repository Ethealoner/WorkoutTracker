using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutTracker.Core.Models
{
    public class WorkoutSession
    {
        public string WorkoutSessionId { get; set; }

        public double WorkoutScore { get; set; }

        public DateTime WorkoutDate { get; set; }
        public string ApplicationUserId { get; set; }

        public virtual ICollection<Exercise>? Exercise { get; set; }

        public WorkoutSession(string workoutSessionId, double workoutScore, DateTime workoutDate, string applicationUserId)
        {
            WorkoutSessionId = workoutSessionId;
            WorkoutScore = workoutScore;
            WorkoutDate = workoutDate;
            ApplicationUserId = applicationUserId;
        }

        public WorkoutSession(string workoutSessionId, double workoutScore, DateTime workoutDate, string applicationUserId, ICollection<Exercise>? exercise)
            : this(workoutSessionId, workoutScore, workoutDate, applicationUserId)
        {
            Exercise = exercise;
        }
    }
}

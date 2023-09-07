using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Application.Interfaces
{
    public interface IWorkoutSessionRepository : IGenericRepository<WorkoutSession>
    {
        void UpdateWorkoutSession(WorkoutSession session);
        IEnumerable<WorkoutSession> GetWorkoutSessionsByUserId(string userId);
        WorkoutSession GetWorkoutSessionWithExercisesById(string workoutSessionId);
        bool UpdateWorkoutSessionScore(float score, string workoutSessionId);
        bool UpdateWorkoutSessionWeight(double weight, string workoutSessionId);

        IEnumerable<WorkoutSession> GetWeightsWithDates(string userId);
        IEnumerable<WorkoutSession> GetWorkoutSessionsScoresWithDates(string userId);
    }
}
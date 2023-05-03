using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Application.Interfaces
{
    public interface IWorkoutSessionRepository : IGenericRepository<WorkoutSession>
    {
        void UpdateWorkoutSession(WorkoutSession session);
        IEnumerable<WorkoutSession> GetWorkoutSessionsByUserId(string userId);
    }
}
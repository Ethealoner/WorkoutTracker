using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Application.Interfaces
{
    public interface IWorkoutSessionRepository : IGenericRepository<WorkoutSession>
    {
        void UpdateWorkoutSessionAsync(WorkoutSession session);
    }
}
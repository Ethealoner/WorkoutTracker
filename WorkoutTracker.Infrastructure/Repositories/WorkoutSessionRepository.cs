using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Core.Models;
using WorkoutTracker.Application.Interfaces;
using System.Linq.Expressions;
using WorkoutTracker.Infrastructure.Repositories;
using WorkoutTracker.Infrastructure.Persistance;

namespace WorkoutTracker.Infrastructure
{
    public class WorkoutSessionRepository : GenericRepository<WorkoutSession>, IWorkoutSessionRepository
    {
        public WorkoutSessionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<WorkoutSession> GetWorkoutSessionsByUserId(string userId)
        {
            return _context.sessions.Where(x => x.ApplicationUserId == userId).ToList();
        }

        public WorkoutSession GetWorkoutSessionWithExercisesById(string workoutSessionId)
        {
            return _context.sessions
                .Where(s => s.WorkoutSessionId == workoutSessionId)
                .Include(s => s.Exercise)
                .FirstOrDefault();
        }

        public void UpdateWorkoutSession(WorkoutSession session)
        {
            throw new NotImplementedException();
        }
    }
}

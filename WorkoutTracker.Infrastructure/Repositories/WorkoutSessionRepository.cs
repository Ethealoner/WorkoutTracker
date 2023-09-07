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

        public IEnumerable<WorkoutSession> GetWeightsWithDates(string userId)
        {
            var sessions = _context.sessions
                .Where(x => x.ApplicationUserId == userId && x.Weight > 0)
                .Select(x => new WorkoutSession 
                {
                   WorkoutDate = x.WorkoutDate,
                   Weight = x.Weight
                })
                .OrderByDescending(x => x.WorkoutDate)
                .ToList();

            return sessions ;
        }

        public IEnumerable<WorkoutSession> GetWorkoutSessionsByUserId(string userId)
        {
            return _context.sessions
                .Where(x => x.ApplicationUserId == userId)
                .OrderByDescending(x => x.WorkoutDate)
                .ToList();
        }

        public IEnumerable<WorkoutSession> GetWorkoutSessionsScoresWithDates(string userId)
        {
            var sessions = _context.sessions
                .Where(x => x.ApplicationUserId == userId && x.WorkoutScore > 0)
                .Select(x => new WorkoutSession {
                    WorkoutDate = x.WorkoutDate,
                    WorkoutScore = x.WorkoutScore
                     })
                .OrderByDescending(x => x.WorkoutDate)
                .ToList();

            return sessions;
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

        public bool UpdateWorkoutSessionScore(float score, string workoutSessionId)
        {
            var workoutSession = _context.sessions.FirstOrDefault(s => s.WorkoutSessionId == workoutSessionId);
            if (workoutSession == null)
                return false;

            workoutSession.WorkoutScore += score;
            return true;
        }

        public bool UpdateWorkoutSessionWeight(double weight, string workoutSessionId)
        {
            var workoutSession = _context.sessions.FirstOrDefault(s => s.WorkoutSessionId == workoutSessionId);
            if (workoutSession == null)
                return false;

            workoutSession.Weight = weight;
            return true;
        }
    }
}

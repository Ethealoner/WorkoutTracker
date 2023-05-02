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

        public void UpdateWorkoutSessionAsync(WorkoutSession session)
        {
            throw new NotImplementedException();
        }
    }
}

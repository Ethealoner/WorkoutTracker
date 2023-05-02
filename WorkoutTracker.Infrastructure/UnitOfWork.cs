using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Infrastructure.Persistance;
using WorkoutTracker.Infrastructure.Repositories;

namespace WorkoutTracker.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IWorkoutSessionRepository WorkoutSessions { get; private set; }
        public IExerciseRepository Exercises { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            WorkoutSessions = new WorkoutSessionRepository(_context);
            Exercises = new ExerciseRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IWorkoutSessionRepository WorkoutSessions { get; }
        IExerciseRepository Exercises { get; }
        
        int Complete();
    }
}

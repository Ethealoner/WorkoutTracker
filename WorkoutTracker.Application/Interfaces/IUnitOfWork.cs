using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IWorkoutSessionRepository WorkoutSessions { get; }
        IExerciseRepository Exercises { get; }
        IExerciseNoteRepository ExerciseNotes { get; }

        bool AddExercise(Exercise exercise);
        bool UpdateExercise(Exercise exercise);
        bool DeleteExercise(Exercise exercise);
        bool CalculateWorkoutSessionScore(Exercise exercise, bool isUpdate);

        bool DeleteWorkoutSession(WorkoutSession workoutSession);
        bool AddWorkoutSession(WorkoutSession workoutSession);
        bool UpdateWorkoutSessionWeight(string workoutSessionId, double weight);

        bool AddOrUpdateExerciseNote(ExerciseNote exerciseNote);

        int Complete();
    }
}

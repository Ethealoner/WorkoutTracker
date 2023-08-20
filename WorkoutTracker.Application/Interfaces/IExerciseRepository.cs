using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Core.Aggregates;
using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Application.Interfaces
{
    public interface IExerciseRepository : IGenericRepository<Exercise>
    {
        public IEnumerable<Exercise> GetAllExercisesBySessionWorkoutId(string sessionWorkoutId);
        public Exercise Find(int exerciseId, string workoutSessionId);
        public bool UpdateExercise(Exercise exercise);
        public Exercise GetExerciseSets(int exerciseId, string workoutSessionId);
        public IEnumerable<Set> GetBestExerciseSets(string userId, string exerciseName);
        public IEnumerable<Set> GetLatestExerciseSets(string userId, string exerciseName);
        public IEnumerable<ExerciseWithDate> GetExercisesWithDatesByName(string userId, string exerciseName);
        public IEnumerable<String> GetExerciseNames(string userId);
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Core.Aggregates;
using WorkoutTracker.Core.Models;
using WorkoutTracker.Infrastructure.Persistance;

namespace WorkoutTracker.Infrastructure.Repositories
{
    public class ExerciseRepository : GenericRepository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Exercise Find(int exerciseId, string workoutSessionId)
        {
            return _context.exercises.Find(exerciseId, workoutSessionId);
        }

        public IEnumerable<Exercise> GetAllExercisesBySessionWorkoutId(string sessionWorkoutId)
        {
            return _context.exercises.Where(x => x.WorkoutSessionId == sessionWorkoutId).ToList();
        }

        public IEnumerable<Set> GetBestExerciseSets(string userId, string exerciseName)
        {
            //TODO optimize this
            var bestExerciseSets = _context.sessions
                .Where(s => s.ApplicationUserId == userId)
                .SelectMany(s => s.Exercise, (s, e) =>
                    new
                    {
                        e.Name,
                        e.Sets,
                        e.ExerciseScore
                    }).Where(e => e.Name == exerciseName)
                .OrderByDescending(s => s.ExerciseScore)
                .FirstOrDefault();
                //.Sets;

            return bestExerciseSets?.Sets;


        }

        public IEnumerable<Set> GetLatestExerciseSets(string userId, string exerciseName)
        {
            var bestExerciseSets = _context.sessions
                .Where(s => s.ApplicationUserId == userId)
                .OrderByDescending(s => s.WorkoutDate)
                .SelectMany(s => s.Exercise, (s, e) =>
                    new
                    {
                        e.Name,
                        e.Sets,
                        e.ExerciseScore
                    })
                .Where(e => e.Name == exerciseName)
                .FirstOrDefault();

            return bestExerciseSets?.Sets;
        }

        public Exercise GetExerciseSets(int exerciseId, string workoutSessionId)
        {
            return _context.exercises.Where(e => e.ExerciseId == exerciseId && e.WorkoutSessionId == workoutSessionId).Select(e => new Exercise { Sets = e.Sets, ExerciseType = e.ExerciseType}).FirstOrDefault();
        }


        public bool UpdateExercise(Exercise exercise)
        {
            Exercise exerciseToBeUpdated = _context.exercises.Find(exercise.ExerciseId, exercise.WorkoutSessionId);
            exerciseToBeUpdated.Sets = exercise.Sets;
            exerciseToBeUpdated.ExerciseType = exercise.ExerciseType;
            exerciseToBeUpdated.Name = exercise.Name;
            exerciseToBeUpdated.ExerciseId = exercise.ExerciseId;
            exerciseToBeUpdated.WorkoutSessionId = exercise.WorkoutSessionId;
            exerciseToBeUpdated.CalculateSetScore();
            _context.exercises.Update(exerciseToBeUpdated);
            return true;
        }

        public IEnumerable<ExerciseWithDate> GetExercisesWithDatesByName(string userId, string exerciseName)
        {
            var exercisesWithDate = _context.sessions
                .Where(s => s.ApplicationUserId == userId)
                .OrderByDescending(s => s.WorkoutDate)
                .SelectMany(s => s.Exercise, (s, e) =>
                    new ExerciseWithDate
                    {
                        Exercise = e,
                        ExerciseDate = s.WorkoutDate
                    })
                .Where(e => e.Exercise.Name == exerciseName)
                .ToList();

            return exercisesWithDate;
        }

        public IEnumerable<string> GetExerciseNames(string userId)
        {
            var exerciseNames = _context.sessions
                .Where(s => s.ApplicationUserId == userId)
                .SelectMany(s => s.Exercise, (s, e) =>
                new string(e.Name))
                .Distinct()
                .ToList();

            return exerciseNames;
        }
    }
}

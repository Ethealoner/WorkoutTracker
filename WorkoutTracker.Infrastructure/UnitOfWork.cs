using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Core.Enums;
using WorkoutTracker.Core.Models;
using WorkoutTracker.Infrastructure.Persistance;
using WorkoutTracker.Infrastructure.Repositories;

namespace WorkoutTracker.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IWorkoutSessionRepository WorkoutSessions { get; private set; }
        public IExerciseRepository Exercises { get; private set; }

        public IExerciseNoteRepository ExerciseNotes { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            WorkoutSessions = new WorkoutSessionRepository(_context);
            Exercises = new ExerciseRepository(_context);
            ExerciseNotes = new ExerciseNoteRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public bool DeleteExercise(Exercise exercise)
        {
            exercise.Sets.Clear();

            if (CalculateWorkoutSessionScore(exercise, true) == false)
                return false;

            Exercises.Remove(exercise);

            return Complete() > 0 ? true : false;
        }

        public bool AddExercise(Exercise exercise)
        {
            if (CalculateWorkoutSessionScore(exercise, false) == false)
                return false;

            Exercises.Add(exercise);

            return Complete() > 0 ? true : false;
        }

        public bool UpdateExercise(Exercise exercise)
        {
            if (CalculateWorkoutSessionScore(exercise, true) == false)
                return false;

            if (Exercises.UpdateExercise(exercise) == false)
                return false;

            return Complete() > 0 ? true : false;
        }

        public bool CalculateWorkoutSessionScore(Exercise exercise, bool isUpdate)
        {
            float difference = 0;
            if (isUpdate)
            {
                // Get old exercise data
                var oldExerciseSet = Exercises.GetExerciseSets(exercise.ExerciseId, exercise.WorkoutSessionId);
                if (oldExerciseSet == null)
                    return false;

                difference = exercise.CalculateSetScore() - oldExerciseSet.CalculateSetScore();
            } else
            {
                difference = exercise.CalculateSetScore();
            }
            if (WorkoutSessions.UpdateWorkoutSessionScore(difference, exercise.WorkoutSessionId) == false)
                return false;

            return true;
        }


        public bool DeleteWorkoutSession(WorkoutSession workoutSession)
        {
            WorkoutSessions.Remove(workoutSession);

            return Complete() > 0 ? true : false;
        }

        public bool AddWorkoutSession(WorkoutSession workoutSession)
        {
            WorkoutSessions.Add(workoutSession);

            return Complete() > 0 ? true : false;
        }

        public bool UpdateWorkoutSessionWeight(string workoutSessionId, double weight)
        {
            WorkoutSessions.UpdateWorkoutSessionWeight(weight, workoutSessionId);

            return Complete() > 0 ? true : false;
        }

        public bool AddOrUpdateExerciseNote(ExerciseNote exerciseNote)
        {
            ExerciseNotes.AddOrUpdate(exerciseNote);

            return Complete() > 0 ? true : false;
        }
    }
}

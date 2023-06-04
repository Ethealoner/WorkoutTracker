using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkoutTracker.Core.Enums;

namespace WorkoutTracker.Core.Models
{
    public class Exercise
    {
        public int ExerciseId { get; set; }

        public string WorkoutSessionId { get; set; }

        public string Name { get; set; }

        public TypeOfExercise ExerciseType { get; set; }

        public float ExerciseScore { get; set; }

        public ICollection<Set> Sets { get; set; }

        public virtual WorkoutSession WorkoutSession { get; set; }

        public Exercise()
        {

        }
        public Exercise(int exerciseId, string workoutSessionId, string name, TypeOfExercise exerciseType)
        {
            ExerciseId = exerciseId;
            WorkoutSessionId = workoutSessionId;
            Name = name;
            ExerciseType = exerciseType;
        }

        public Exercise(int exerciseId, string workoutSessionId, string name, TypeOfExercise exerciseType, WorkoutSession workoutSession, ICollection<Set> sets)
            : this(exerciseId, workoutSessionId, name, exerciseType)
        {
            WorkoutSession = workoutSession;
            Sets = sets;
        }

        public float CalculateSetScore()
        {
            float score = 0;

            foreach (Set set in Sets)
            {
                if (ExerciseType == TypeOfExercise.Weigth)
                {
                    score += set.Repetitions + (set.Difficulty * 2);
                }
                else
                {
                    score += set.Repetitions / ((set.Difficulty + 1) * 2);
                }
            }
            ExerciseScore = score;

            return ExerciseScore;
        }
    }

    public class Set
    {
        public int Repetitions { get; set; }
        public float Difficulty { get; set; }

        public Set(int repetitions, float difficulty)
        {
            Repetitions = repetitions;
            Difficulty = difficulty;
        }

        public Set()
        {

        }
    }
}

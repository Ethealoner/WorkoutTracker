using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Tests.UnitTests.Common.TestData
{
    public static class CommonTestData
    {
        public static IList<WorkoutSession> workoutSessions;
        public static IList<Exercise> exercises;
        public static IList<string> userIds;


        static CommonTestData()
        {
            CreateUserIds();
            CreateWorkoutSessions();
            CreateExercises();

        }
        public static void CreateUserIds()
        {
            userIds = new List<string>()
            {
                "1",
                "2",
                "3"
            };
        }

        public static void CreateExercises()
        {
            exercises = new List<Exercise>()
            {
                new Exercise(1, workoutSessions[0].WorkoutSessionId, "PushUps", WorkoutTracker.Core.Enums.TypeOfExercise.Weigth)
                {
                    WorkoutSession = workoutSessions[0],
                    Sets = new List<Set>()
                    {
                        new Set(10, 15),
                        new Set(9, 16),
                        new Set(8, 16)
                    }
                },
                new Exercise(2, workoutSessions[0].WorkoutSessionId, "PullUps", WorkoutTracker.Core.Enums.TypeOfExercise.Weigth)
                {
                    WorkoutSession = workoutSessions[0],
                    Sets = new List<Set>()
                    {
                        new Set(5, 86),
                        new Set(7, 86)
                    }
                },
                new Exercise(1, workoutSessions[2].WorkoutSessionId, "Running", WorkoutTracker.Core.Enums.TypeOfExercise.Cardio)
                {
                    WorkoutSession = workoutSessions[2],
                    Sets = new List<Set>()
                    {
                        new Set(6000, 34)
                    }
                }
            };

            //workoutSessions[0].Exercise.Add(exercises[0]);
           // workoutSessions[0].Exercise.Add(exercises[1]);
           // workoutSessions[2].Exercise.Add(exercises[2]);
        }

        public static void CreateWorkoutSessions()
        {
            workoutSessions = new List<WorkoutSession>()
            {
                new WorkoutSession("1", 150.12, DateTime.Now, userIds[0]),
                new WorkoutSession("2", 0.0, DateTime.Now, userIds[0]),
                new WorkoutSession("3", 11.1, DateTime.Now, userIds[1])
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Application.DTOs.WorkoutSession
{
    public class WorkoutSessionDto
    {
        public string WorkoutSessionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public double workoutSessionScore { get; set; }
        public List<ExerciseSummaryDto> exerciseSummaries { get; set; }
    }

    public class ExerciseSummaryDto
    {
        public string ExerciseName { get; set; }
        public int ExerciseId { get; set; }

        public ExerciseSummaryDto(string exerciseName, int exerciseId)
        {
            ExerciseName = exerciseName;
            ExerciseId = exerciseId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Core.Aggregates
{
    public record ExerciseWithDate
    {
        public Exercise Exercise { get; set; }
        public DateTime ExerciseDate { get; set; }
    }
}

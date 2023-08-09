using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Core.Models
{
    public class ExerciseNote
    {
       public string ApplicationUserId { get; set; }
       public string ExerciseName { get; set; }
       public string Notes { get; set; }
    }
}

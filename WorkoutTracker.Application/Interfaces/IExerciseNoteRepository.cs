using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Application.Interfaces
{
    public interface IExerciseNoteRepository : IGenericRepository<ExerciseNote>
    {
        public ExerciseNote Find(string applicationUserId, string exerciseName);
        public void AddOrUpdate(ExerciseNote exerciseNote);
    }
}

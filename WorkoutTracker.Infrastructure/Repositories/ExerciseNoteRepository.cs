using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Core.Models;
using WorkoutTracker.Infrastructure.Persistance;

namespace WorkoutTracker.Infrastructure.Repositories
{
    public class ExerciseNoteRepository : GenericRepository<ExerciseNote>, IExerciseNoteRepository
    {
        public ExerciseNoteRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void AddOrUpdate(ExerciseNote exerciseNote)
        {
            if (_context.exerciseNotes.Any(x => x.ExerciseName == exerciseNote.ExerciseName && x.ApplicationUserId == exerciseNote.ApplicationUserId))
            {
                _context.exerciseNotes.Update(exerciseNote);
            }
            else
            {
                _context.exerciseNotes.Add(exerciseNote);
            }
                
        }

        public ExerciseNote Find(string applicationUserId, string exerciseName)
        {
            return _context.exerciseNotes.Find(applicationUserId, exerciseName);
        }
    }
}

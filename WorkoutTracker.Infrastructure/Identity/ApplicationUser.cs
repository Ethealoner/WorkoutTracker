using Microsoft.AspNetCore.Identity;
using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<WorkoutSession>? WorkoutSessions { get; set; }
        public virtual ICollection<ExerciseNote>? ExerciseNotes { get; set; }

    }
}

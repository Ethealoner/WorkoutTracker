using System.ComponentModel.DataAnnotations;
using WorkoutTracker.Core.Models;

namespace WorkoutTrackerMvc.Models.WorkoutSessionViewModels
{
    public class WorkoutSessionDetailViewModel
    {
        public DateTime date { get; set; }
        public string workoutSessionId { get; set; }
        public ICollection<Exercise> exercises { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public double weight { get; set; }
    }
}

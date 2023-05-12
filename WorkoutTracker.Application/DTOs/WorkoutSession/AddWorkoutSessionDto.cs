using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Application.DTOs.WorkoutSession
{
    public class AddWorkoutSessionDto
    {
        [Required(ErrorMessage = "User Id is required")]
        public string userId { get; set; }
        [Required(ErrorMessage = "Workout session date is required")]
        public DateTime createdAt { get; set; }
    }
}

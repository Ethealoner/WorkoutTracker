using Microsoft.AspNetCore.Mvc;


namespace WorkoutTrackerMvc.Controllers
{
    public class ExerciseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}

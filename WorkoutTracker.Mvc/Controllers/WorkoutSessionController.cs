using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Core.Models;

namespace WorkoutTrackerMvc.Controllers
{
    [Authorize]
    public class WorkoutSessionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkoutSessionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult GetWorkoutSessions(string Id)
        {
            var sessions = _unitOfWork.WorkoutSessions.GetAll();
            return View(sessions);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

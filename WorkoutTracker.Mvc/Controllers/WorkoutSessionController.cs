using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Application.Commands.WorkoutSessions;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Application.Queries.WorkoutSessions;
using WorkoutTracker.Core.Models;
using WorkoutTrackerMvc.Models.WorkoutSessionViewModels;

namespace WorkoutTrackerMvc.Controllers
{
    [Authorize]
    public class WorkoutSessionController : Controller
    {
        private readonly IMediator _mediator;

        public WorkoutSessionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> GetWorkoutSessions(string Id)
        {
            var sessions = await _mediator.Send(new GetWorkoutSessionsByUserIdQuery(Id));

            return View(new WorkoutSessionsWithUserViewModel { UserId = Id, workoutSessions = sessions });
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkoutSession(string userId)
        {
            WorkoutSession session = new WorkoutSession(Guid.NewGuid().ToString(), 0.0, DateTime.Now, userId);
            await _mediator.Send(new AddWorkoutSessionCommand(session));

            return RedirectToAction("GetWorkoutSessions", new { Id = userId });
        }

        public async Task<IActionResult> DeleteWorkoutSession(string workoutSessionId)
        {
            WorkoutSession session = await _mediator.Send(new GetWorkoutSessionByIdQuery(workoutSessionId));
            await _mediator.Send(new DeleteWorkoutSessionCommand(session));

            return RedirectToAction("GetWorkoutSessions", new { Id = session.ApplicationUserId });
        }

        public async Task<IActionResult> WorkoutSessionDetail(string workoutSessionId)
        {
            WorkoutSession session = await _mediator.Send(new GetWorkoutSessionWithExercisesByIdQuery(workoutSessionId));
            WorkoutSessionDetailViewModel sessionDetail = new WorkoutSessionDetailViewModel() 
            { date = session.WorkoutDate, 
              workoutSessionId = session.WorkoutSessionId, 
              exercises = session.Exercise };

            return View(sessionDetail);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

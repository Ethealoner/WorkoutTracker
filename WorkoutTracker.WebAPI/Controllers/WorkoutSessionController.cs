using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Application.Commands.WorkoutSessions;
using WorkoutTracker.Application.Queries.WorkoutSessions;
using WorkoutTracker.Core.Models;
using WorkoutTracker.WebAPI.Models;

namespace WorkoutTracker.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutSessionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkoutSessionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("/api/WorkoutSession/{userId}")]
        public async Task<IActionResult> GetUsersWorkoutSessions([FromRoute] string userId)
        {
            var workoutSessions = await _mediator.Send(new GetWorkoutSessionsByUserIdQuery(userId));

            return Ok(workoutSessions);
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkoutSession([FromBody] AddWorkoutSessionModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            WorkoutSession newSession = new WorkoutSession()
            {
                ApplicationUserId = model.userId,
                WorkoutDate = model.createdAt
            };

            var isSuccessfullyCreated = await _mediator.Send(new AddWorkoutSessionCommand(newSession));

            if (isSuccessfullyCreated)
                return Ok();

            return BadRequest("Unable to create new workout session");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteWorkoutSession([FromRoute] string id)
        {
            var workoutSession = await _mediator.Send(new GetWorkoutSessionByIdQuery(id));
            if (workoutSession == null)
                return NotFound("Workout session was not found, unable to delete it");

            var isSuccessfullyDeleted = await _mediator.Send(new DeleteWorkoutSessionCommand(workoutSession));

            if (!isSuccessfullyDeleted)
                return BadRequest("Unable to delete workout session");

            return Ok();
        }

        [HttpGet]
        [Route("/api/workoutSessionDetails/{id}")]
        public async Task<IActionResult> GetWorkoutSessionDetails(string id)
        {
            var workoutSession = await _mediator.Send(new GetWorkoutSessionWithExercisesByIdQuery(id));
            if (workoutSession == null)
                return NotFound("Workout session was not found");

            List<ExerciseSummary> exerciseSummaries = new List<ExerciseSummary>();
            foreach(Exercise exercise in workoutSession.Exercise)
            {
                exerciseSummaries.Add(new ExerciseSummary(exercise.Name, exercise.ExerciseId));
            }

            WorkoutSessionDetails sessionDetails = new WorkoutSessionDetails()
            {
                WorkoutSessionId = workoutSession.WorkoutSessionId,
                CreatedAt = workoutSession.WorkoutDate,
                exerciseSummaries = exerciseSummaries
            };

            return Ok(sessionDetails);
        }
    }
}

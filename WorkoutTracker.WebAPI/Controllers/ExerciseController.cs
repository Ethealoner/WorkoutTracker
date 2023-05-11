using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using WorkoutTracker.Application.Commands.Exercises;
using WorkoutTracker.Application.Queries.Exercises;
using WorkoutTracker.Core.Enums;
using WorkoutTracker.Core.Models;
using WorkoutTracker.WebAPI.Models;

namespace WorkoutTracker.WebAPI.Controllers
{
    [Route("api/exercise")]
    [ApiController]
    [Authorize]
    public class ExerciseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExerciseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{workoutSessionId}/{exerciseId}")]
        public async Task<IActionResult> GetExercise(string workoutSessionid, int exerciseId)
        {
            var exercise = await _mediator.Send(new GetExerciseByIdQuery(exerciseId, workoutSessionid));

            if (exercise == null)
                return NotFound("Exercise was not found");


            ExerciseModel model = new ExerciseModel()
            {
                exerciseId = exercise.ExerciseId,
                exerciseName = exercise.Name,
                typeOfExercise = exercise.ExerciseType.ToString(),
                workoutSessionId = exercise.WorkoutSessionId,
                repetitions = JsonSerializer.Serialize(exercise.Sets)
            };
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddExercise([FromBody] ExerciseModel exerciseModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Exercise newExercise = new Exercise()
            {
                ExerciseType = (TypeOfExercise) Enum.Parse(typeof(TypeOfExercise),exerciseModel.typeOfExercise),
                Name = exerciseModel.exerciseName,
                Sets = JsonSerializer.Deserialize<ICollection<Set>>(exerciseModel.repetitions),
                WorkoutSessionId = exerciseModel.workoutSessionId
            };

            var result = await _mediator.Send(new AddExerciseCommand(newExercise));

            if (!result)
                return BadRequest("Unable to add exercise");

            return Ok();
        }

        [HttpDelete]
        [Route("{workoutSessionId}/{exerciseId}")]
        public async Task<IActionResult> DeleteExercise(string workoutSessionid, int exerciseId)
        {
            var exercise = await _mediator.Send(new GetExerciseByIdQuery(exerciseId, workoutSessionid));

            if (exercise == null)
                return NotFound("Exercise was not found");

            var result = await _mediator.Send(new DeleteExerciseCommand(exercise));

            if (!result)
                return BadRequest("Unable to delete exercise");

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExercise([FromBody] ExerciseModel exerciseModel)
        {
            var exercise = await _mediator.Send(new GetExerciseByIdQuery(exerciseModel.exerciseId, exerciseModel.workoutSessionId));

            if (exercise == null)
                return NotFound("Exercise was not found");

            exercise.ExerciseType = (TypeOfExercise)Enum.Parse(typeof(TypeOfExercise), exerciseModel.typeOfExercise);
            exercise.Sets = JsonSerializer.Deserialize<ICollection<Set>>(exerciseModel.repetitions);
            exercise.Name = exerciseModel.exerciseName;
            exercise.ExerciseId = exercise.ExerciseId;

            var result = await _mediator.Send(new UpdateExerciseCommand(exercise));

            if (!result)
                return BadRequest("Unable to update exercise");

            return Ok();
        }
    }
}

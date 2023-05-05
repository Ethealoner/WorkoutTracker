using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Application.Commands.Exercises;
using WorkoutTracker.Application.Queries.Exercises;
using WorkoutTracker.Core.Models;
using WorkoutTrackerMvc.Models.ExerciseViewModels;

namespace WorkoutTrackerMvc.Controllers
{
    [Authorize]
    public class ExerciseController : Controller
    {
        private readonly IMediator _mediator;

        public ExerciseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NewExerciseDetail(string workoutSessionId)
        {
            CreateExerciseViewModel createExerciseViewModel = new CreateExerciseViewModel() { workoutSessionId = workoutSessionId};
            return View(createExerciseViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> NewExerciseDetail(CreateExerciseViewModel createExerciseViewModel)
        {
            if (createExerciseViewModel.setToAdd.Repetitions != 0 && createExerciseViewModel.setToAdd.Difficulty != 0)
            {
                Set newSet = new Set(createExerciseViewModel.setToAdd.Repetitions, createExerciseViewModel.setToAdd.Difficulty);
                createExerciseViewModel.sets.Add(newSet);
                createExerciseViewModel.setToAdd.Difficulty = 0;
                createExerciseViewModel.setToAdd.Repetitions = 0;
            }

            return View(createExerciseViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> SaveExercise(CreateExerciseViewModel createExerciseViewModel)
        {
            Exercise exercise = new Exercise();
            exercise.WorkoutSessionId = createExerciseViewModel.workoutSessionId;
            exercise.Name = createExerciseViewModel.exerciseName;
            exercise.ExerciseType = createExerciseViewModel.ExerciseType;
            exercise.Sets = createExerciseViewModel.sets;
            await _mediator.Send(new AddExerciseCommand(exercise));

            return RedirectToAction("WorkoutSessionDetail", "WorkoutSession", new { workoutSessionId  = createExerciseViewModel.workoutSessionId});
        }

        public async Task<IActionResult> DeleteExercise(int exerciseId, string workoutSessionId)
        {
            Exercise exerciseToDelete = await _mediator.Send(new GetExerciseByIdQuery(exerciseId, workoutSessionId));
            await _mediator.Send(new DeleteExerciseCommand(exerciseToDelete));

            return RedirectToAction("WorkoutSessionDetail", "WorkoutSession", new { workoutSessionId = workoutSessionId });
        }

    }
}

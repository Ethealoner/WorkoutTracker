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
        public IActionResult ExerciseDetail(string Id)
        {
            CreateExerciseViewModel createExerciseViewModel = new CreateExerciseViewModel() { workoutSessionId = Id};
            return View(createExerciseViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ExerciseDetail(CreateExerciseViewModel createExerciseViewModel)
        {
            if (!ModelState.IsValid)
                return View(createExerciseViewModel);

            Set newSet = new Set(createExerciseViewModel.setToAdd.Repetitions, createExerciseViewModel.setToAdd.Difficulty);
            createExerciseViewModel.sets.Add(newSet);
            createExerciseViewModel.setToAdd.Difficulty = 0;
            createExerciseViewModel.setToAdd.Repetitions = 0;

            return View(createExerciseViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditExerciseDetail(int exerciseId, string workoutSessionId)
        {
            Exercise exercise = await _mediator.Send(new GetExerciseByIdQuery(exerciseId, workoutSessionId));
            return View("ExerciseDetail",new CreateExerciseViewModel()
            {
                exerciseId = exercise.ExerciseId,
                ExerciseType = exercise.ExerciseType,
                exerciseName = exercise.Name,
                sets = exercise.Sets.ToList(),
                workoutSessionId = exercise.WorkoutSessionId,
            });
        }


        [HttpPost]
        public async Task<IActionResult> SaveExercise(CreateExerciseViewModel createExerciseViewModel)
        {
            if (ModelState.IsValid)
            {
                Exercise exercise = new Exercise();
                exercise.WorkoutSessionId = createExerciseViewModel.workoutSessionId;
                exercise.Name = createExerciseViewModel.exerciseName;
                exercise.ExerciseType = createExerciseViewModel.ExerciseType;
                exercise.Sets = createExerciseViewModel.sets;
                if (createExerciseViewModel.exerciseId != null)
                {
                    exercise.ExerciseId = (int)createExerciseViewModel.exerciseId;
                    await _mediator.Send(new UpdateExerciseCommand(exercise));
                }
                else
                {
                    await _mediator.Send(new AddExerciseCommand(exercise));
                }
                return RedirectToAction("WorkoutSessionDetail", "WorkoutSession", new { workoutSessionId = createExerciseViewModel.workoutSessionId });
            }
            return View("ExerciseDetail", createExerciseViewModel);
        }

        public async Task<IActionResult> DeleteExercise(int exerciseId, string workoutSessionId)
        {
            Exercise exerciseToDelete = await _mediator.Send(new GetExerciseByIdQuery(exerciseId, workoutSessionId));
            await _mediator.Send(new DeleteExerciseCommand(exerciseToDelete));

            return RedirectToAction("WorkoutSessionDetail", "WorkoutSession", new { workoutSessionId = workoutSessionId });
        }

    }
}

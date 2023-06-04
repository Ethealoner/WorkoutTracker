using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        [HttpGet]
        public async Task<IActionResult> EditExerciseDetail(int exerciseId, string workoutSessionId)
        {
            Exercise exercise = await _mediator.Send(new GetExerciseByIdQuery(exerciseId, workoutSessionId));
            return View("ExerciseDetail",new CreateExerciseViewModel()
            {
                exerciseId = exercise.ExerciseId,
                ExerciseType = exercise.ExerciseType,
                exerciseName = exercise.Name,
                Sets = exercise.Sets,
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
                exercise.Sets = createExerciseViewModel.Sets.Where(s => s != null).ToList();
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

        public  PartialViewResult AddNewSet()
        {
            return PartialView("~/Views/Exercise/EditorTemplates/Set.cshtml", new Set());
        }

        [HttpGet]
        public async Task<PartialViewResult> GetBestSets(string exerciseName)
        {
            string userId = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var bestSets = await _mediator.Send(new GetBestExerciseSetsQuery(exerciseName, userId));
            return PartialView("~/Views/Exercise/Partial/BestExercise.cshtml", bestSets);
        }

    }
}

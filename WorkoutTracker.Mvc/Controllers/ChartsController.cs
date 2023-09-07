using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using WorkoutTracker.Application.Queries.Exercises;
using WorkoutTracker.Application.Queries.WorkoutSessions;
using WorkoutTrackerMvc.Models.ChartsViewModels;

namespace WorkoutTrackerMvc.Controllers
{
    [Authorize]
    public class ChartsController : Controller
    {
        private readonly IMediator _mediator;

        public ChartsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            string userId = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var exerciseNames = new List<string>();
            var readExerciseNames = await _mediator.Send(new GetExerciseNamesQuery(userId));

            if (readExerciseNames != null)
                exerciseNames = (List<string>) readExerciseNames;

            return View(exerciseNames);
        }

        [HttpGet]
        public async Task<ContentResult> GetChartData(string exerciseName)
        {
            string userId = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var bestSets = await _mediator.Send(new GetExercisesWithDateByNameQuery(exerciseName, userId));

            
            List<ChartViewModel> chartData = new List<ChartViewModel>();
            foreach(var exerciseWithDate in bestSets )
            {
                chartData.Add(new ChartViewModel()
                {
                    X = exerciseWithDate.Exercise.ExerciseScore,
                    Y = exerciseWithDate.ExerciseDate,
                });
            }

            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return Content(JsonConvert.SerializeObject(chartData, _jsonSetting), "application/json");
        }

        [HttpGet]
        public async Task<ContentResult> GetWeightChartData()
        {
            string userId = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var bestSets = await _mediator.Send(new GetWeightsWithDatesQuery(userId));


            List<ChartViewModel> chartData = new List<ChartViewModel>();
            foreach (var exerciseWithDate in bestSets)
            {
                chartData.Add(new ChartViewModel()
                {
                    X = exerciseWithDate.Weight,
                    Y = exerciseWithDate.WorkoutDate,
                });
            }

            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return Content(JsonConvert.SerializeObject(chartData, _jsonSetting), "application/json");
        }

        [HttpGet]
        public async Task<ContentResult> GetScoreChartData()
        {
            string userId = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var bestSets = await _mediator.Send(new GetWorkoutSessionsScoresWithDatesQuery(userId));


            List<ChartViewModel> chartData = new List<ChartViewModel>();
            foreach (var exerciseWithDate in bestSets)
            {
                chartData.Add(new ChartViewModel()
                {
                    X = exerciseWithDate.WorkoutScore,
                    Y = exerciseWithDate.WorkoutDate,
                });
            }

            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return Content(JsonConvert.SerializeObject(chartData, _jsonSetting), "application/json");
        }
    }
}

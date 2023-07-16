using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using WorkoutTracker.Application.Queries.Exercises;
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

        public IActionResult Index()
        {
            return View();
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
    }
}

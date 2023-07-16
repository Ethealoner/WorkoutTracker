using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Core.Aggregates;
using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Application.Queries.Exercises
{
    public class GetExercisesWithDateByNameQuery : IRequest<IEnumerable<ExerciseWithDate>>
    {
        public string ExerciseName { get; private set; }
        public string UserId { get; private set; }

        public GetExercisesWithDateByNameQuery(string exerciseName, string userId)
        {
            ExerciseName = exerciseName;
            UserId = userId;
        }
    }

    public class GetExercisesByNameQueryHandler : IRequestHandler<GetExercisesWithDateByNameQuery, IEnumerable<ExerciseWithDate>>
    {
        private readonly IExerciseRepository _ExerciseRepository;
        public GetExercisesByNameQueryHandler(IExerciseRepository exerciseRepository)
        {
            _ExerciseRepository = exerciseRepository;
        }

        public Task<IEnumerable<ExerciseWithDate>> Handle(GetExercisesWithDateByNameQuery request, CancellationToken cancellationToken)
        {
            var bestExerciseSets = _ExerciseRepository.GetExercisesWithDatesByName(request.UserId, request.ExerciseName);
            return Task.FromResult(bestExerciseSets);
        }
    }
}

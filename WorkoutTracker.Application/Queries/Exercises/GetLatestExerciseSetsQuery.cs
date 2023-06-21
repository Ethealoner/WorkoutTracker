using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Application.Queries.Exercises
{

    public class GetLatestExerciseSetsQuery : IRequest<IEnumerable<Set>>
    {
        public string ExerciseName { get; private set; }
        public string UserId { get; private set; }

        public GetLatestExerciseSetsQuery(string exerciseName, string userId)
        {
            ExerciseName = exerciseName;
            UserId = userId;
        }
    }

    public class GetLatestExerciseSetsQueryHandler : IRequestHandler<GetLatestExerciseSetsQuery, IEnumerable<Set>>
    {
        private readonly IExerciseRepository _ExerciseRepository;
        public GetLatestExerciseSetsQueryHandler(IExerciseRepository exerciseRepository)
        {
            _ExerciseRepository = exerciseRepository;
        }
        public Task<IEnumerable<Set>> Handle(GetLatestExerciseSetsQuery request, CancellationToken cancellationToken)
        {
            var bestExerciseSets = _ExerciseRepository.GetLatestExerciseSets(request.UserId, request.ExerciseName);
            return Task.FromResult(bestExerciseSets);
        }
    }
}

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
    public class GetBestExerciseSetsQuery : IRequest<IEnumerable<Set>>
    {
        public string ExerciseName { get; private set; }
        public string UserId { get; private set; }

        public GetBestExerciseSetsQuery(string exerciseName, string userId)
        {
            ExerciseName = exerciseName;
            UserId = userId;
        }
    }

    public class GetBestExerciseSetsQueryHandler : IRequestHandler<GetBestExerciseSetsQuery, IEnumerable<Set>>
    {
        private readonly IExerciseRepository _ExerciseRepository;
        public GetBestExerciseSetsQueryHandler(IExerciseRepository exerciseRepository)
        {
            _ExerciseRepository = exerciseRepository;
        }
        public Task<IEnumerable<Set>> Handle(GetBestExerciseSetsQuery request, CancellationToken cancellationToken)
        {
            var bestExerciseSets = _ExerciseRepository.GetBestExerciseSets(request.UserId, request.ExerciseName);
            return Task.FromResult(bestExerciseSets);
        }
    }
}


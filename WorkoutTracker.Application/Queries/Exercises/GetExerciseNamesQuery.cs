using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Application.Interfaces;

namespace WorkoutTracker.Application.Queries.Exercises
{
    public class GetExerciseNamesQuery : IRequest<IEnumerable<string>>
    {
        public string UserId { get; private set; }

        public GetExerciseNamesQuery(string userId)
        {
            UserId = userId;
        }
    }

    public class GetExerciseNamesQueryHandler : IRequestHandler<GetExerciseNamesQuery, IEnumerable<string>>
    {
        private readonly IExerciseRepository ExerciseRepository;

        public GetExerciseNamesQueryHandler(IExerciseRepository exerciseRepository)
        {
            ExerciseRepository = exerciseRepository;
        }

        public Task<IEnumerable<string>> Handle(GetExerciseNamesQuery request, CancellationToken cancellationToken)
        {
            var exerciseNames = ExerciseRepository.GetExerciseNames(request.UserId);
            return Task.FromResult(exerciseNames);
        }
    }
}

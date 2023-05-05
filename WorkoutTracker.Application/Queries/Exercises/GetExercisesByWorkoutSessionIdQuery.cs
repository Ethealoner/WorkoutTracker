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
    public class GetExercisesByWorkoutSessionIdQuery : IRequest<IEnumerable<Exercise>>
    {
        public string SessionWorkoutId { get; private set; }

        public GetExercisesByWorkoutSessionIdQuery(string sessionWorkoutId)
        {
            SessionWorkoutId = sessionWorkoutId;
        }
    }

    public class GetExercisesByWorkoutSessionIdQueryHandler : IRequestHandler<GetExercisesByWorkoutSessionIdQuery, IEnumerable<Exercise>>
    {
        private readonly IExerciseRepository ExerciseRepository;
        public GetExercisesByWorkoutSessionIdQueryHandler(IExerciseRepository exerciseRepository)
        {
            ExerciseRepository = exerciseRepository;
        }
        public Task<IEnumerable<Exercise>> Handle(GetExercisesByWorkoutSessionIdQuery request, CancellationToken cancellationToken)
        {
            var workoutSessions = ExerciseRepository.GetAllExercisesBySessionWorkoutId(request.SessionWorkoutId);
            return Task.FromResult(workoutSessions);
        }
    }
}


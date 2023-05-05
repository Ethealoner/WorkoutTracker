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
    public class GetExerciseByIdQuery : IRequest<Exercise>
    {
        public int ExerciseId { get; private set; }
        public string WorkoutSessionId { get; private set; }

        public GetExerciseByIdQuery(int exerciseId, string workoutSessionId)
        {
            ExerciseId = exerciseId;
            WorkoutSessionId = workoutSessionId;
        }
    }

    public class GetExerciseByIdQueryHandler : IRequestHandler<GetExerciseByIdQuery, Exercise>
    {
        private readonly IExerciseRepository ExerciseRepository;
        public GetExerciseByIdQueryHandler(IExerciseRepository exerciseRepository)
        {
            ExerciseRepository = exerciseRepository;
        }
        public Task<Exercise> Handle(GetExerciseByIdQuery request, CancellationToken cancellationToken)
        {
            var exercise = ExerciseRepository.Find(request.ExerciseId, request.WorkoutSessionId);
            return Task.FromResult(exercise);
        }
    }
}

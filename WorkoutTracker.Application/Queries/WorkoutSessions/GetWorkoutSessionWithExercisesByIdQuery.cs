using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Application.Queries.WorkoutSessions
{
    public class GetWorkoutSessionWithExercisesByIdQuery : IRequest<WorkoutSession>
    {
        public string WorkoutSessionId { get; private set; }

        public GetWorkoutSessionWithExercisesByIdQuery(string workoutSessionId)
        {
            WorkoutSessionId = workoutSessionId;
        }
    }

    public class GetWorkoutSessionWithExercisesByIdQueryHandler : IRequestHandler<GetWorkoutSessionWithExercisesByIdQuery, WorkoutSession>
    {
        private readonly IWorkoutSessionRepository WorkoutSessionRepository;

        public GetWorkoutSessionWithExercisesByIdQueryHandler(IWorkoutSessionRepository workoutSessionRepository)
        {
            WorkoutSessionRepository = workoutSessionRepository;
        }

        public Task<WorkoutSession> Handle(GetWorkoutSessionWithExercisesByIdQuery request, CancellationToken cancellationToken)
        {
            var workoutSession = WorkoutSessionRepository.GetWorkoutSessionWithExercisesById(request.WorkoutSessionId);
            return Task.FromResult(workoutSession);
        }
    }
}

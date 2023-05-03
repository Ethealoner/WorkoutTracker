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
    public class GetWorkoutSessionsByUserIdQuery : IRequest<IEnumerable<WorkoutSession>>
    {
        public string UserId { get; private set; }

        public GetWorkoutSessionsByUserIdQuery(string userId)
        {
            UserId = userId;
        }
    }

    public class GetWorkoutSessionsByUserIdQueryHandler : IRequestHandler<GetWorkoutSessionsByUserIdQuery, IEnumerable<WorkoutSession>>
    {
        private readonly IWorkoutSessionRepository WorkoutSessionRepository;
        public GetWorkoutSessionsByUserIdQueryHandler(IWorkoutSessionRepository workoutSessionRepository)
        {
            WorkoutSessionRepository = workoutSessionRepository;
        }
        public Task<IEnumerable<WorkoutSession>> Handle(GetWorkoutSessionsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var workoutSessions = WorkoutSessionRepository.GetWorkoutSessionsByUserId(request.UserId);
            return Task.FromResult(workoutSessions);
        }
    }
}

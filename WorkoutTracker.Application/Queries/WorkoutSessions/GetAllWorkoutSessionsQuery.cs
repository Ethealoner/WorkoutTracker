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
    public class GetAllWorkoutSessionsQuery : IRequest<IEnumerable<WorkoutSession>>
    {
    }

    public class GetAllWorkoutSessionsQueryHandler : IRequestHandler<GetAllWorkoutSessionsQuery, IEnumerable<WorkoutSession>>
    {
        private readonly IWorkoutSessionRepository WorkoutSessionRepository;

        public GetAllWorkoutSessionsQueryHandler(IWorkoutSessionRepository workoutSessionRepository)
        {
            WorkoutSessionRepository = workoutSessionRepository;
        }

        public Task<IEnumerable<WorkoutSession>> Handle(GetAllWorkoutSessionsQuery request, CancellationToken cancellationToken)
        {
            var workoutSessions = WorkoutSessionRepository.GetAll();
            return Task.FromResult(workoutSessions);
        }
    }
}

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
    public class GetWeightsWithDatesQuery : IRequest<IEnumerable<WorkoutSession>>
    {
        public string UserId { get; set; }

        public GetWeightsWithDatesQuery(string userId)
        {
            UserId = userId;
        }
    }

    public class GetWeightsWithDatesQueryHandler : IRequestHandler<GetWeightsWithDatesQuery, IEnumerable<WorkoutSession>>
    {
        private readonly IWorkoutSessionRepository WorkoutSessionRepository;

        public GetWeightsWithDatesQueryHandler(IWorkoutSessionRepository workoutSessionRepository)
        {
            WorkoutSessionRepository = workoutSessionRepository;
        }

        public Task<IEnumerable<WorkoutSession>> Handle(GetWeightsWithDatesQuery request, CancellationToken cancellationToken)
        {
            var workoutSessions = WorkoutSessionRepository.GetWeightsWithDates(request.UserId);
            return Task.FromResult(workoutSessions);
        }
    }
}

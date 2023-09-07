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
    public class GetWorkoutSessionsScoresWithDatesQuery : IRequest<IEnumerable<WorkoutSession>>
    {
        public string UserId { get; set; }

        public GetWorkoutSessionsScoresWithDatesQuery(string userId)
        {
            UserId = userId;
        }
    }

    public class GetWorkoutSessionsScoresWithDatesQueryHandler : IRequestHandler<GetWorkoutSessionsScoresWithDatesQuery, IEnumerable<WorkoutSession>>
    {
        private readonly IWorkoutSessionRepository WorkoutSessionRepository;

        public GetWorkoutSessionsScoresWithDatesQueryHandler(IWorkoutSessionRepository workoutSessionRepository)
        {
            WorkoutSessionRepository = workoutSessionRepository;
        }

        public Task<IEnumerable<WorkoutSession>> Handle(GetWorkoutSessionsScoresWithDatesQuery request, CancellationToken cancellationToken)
        {
            var workoutSessions = WorkoutSessionRepository.GetWorkoutSessionsScoresWithDates(request.UserId);
            return Task.FromResult(workoutSessions);
        }
    }
}

﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Application.Queries.WorkoutSessions
{
    public class GetWorkoutSessionByIdQuery : IRequest<WorkoutSession>
    {
        public string WorkoutSessionId { get; private set; }

        public GetWorkoutSessionByIdQuery(string workoutSessionId)
        {
            WorkoutSessionId = workoutSessionId;
        }
    }

    public class GetWorkoutSessionByIdQueryHandler : IRequestHandler<GetWorkoutSessionByIdQuery, WorkoutSession>
    {
        public IWorkoutSessionRepository workoutSessionRepository { get; }

        public GetWorkoutSessionByIdQueryHandler(IWorkoutSessionRepository workoutSessionRepository)
        {
            this.workoutSessionRepository = workoutSessionRepository;
        }

        public Task<WorkoutSession> Handle(GetWorkoutSessionByIdQuery request, CancellationToken cancellationToken)
        {
            var workoutSession = workoutSessionRepository.GetById(request.WorkoutSessionId);
           return Task.FromResult(workoutSession);
        }
    }
}

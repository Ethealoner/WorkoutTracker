using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Application.Commands.WorkoutSessions
{

    public class UpdateWorkoutSessionCommand : IRequest<bool>
    {
        public string WorkoutSessionId { get; private set; }
        public double Weight { get; private set; }
        public UpdateWorkoutSessionCommand(string workoutSessionId, double weight)
        {
            WorkoutSessionId = workoutSessionId;
            Weight = weight;
        }
    }

    public class UpdateWorkoutSessionCommandHandler : IRequestHandler<UpdateWorkoutSessionCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateWorkoutSessionCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(UpdateWorkoutSessionCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(unitOfWork.UpdateWorkoutSessionWeight(request.WorkoutSessionId, request.Weight));
        }
    }
}

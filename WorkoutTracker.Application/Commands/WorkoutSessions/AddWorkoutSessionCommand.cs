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
    public class AddWorkoutSessionCommand : IRequest<bool>
    {
        public WorkoutSession workoutSession { get; private set; }
        public AddWorkoutSessionCommand(WorkoutSession workoutSession)
        {
            this.workoutSession = workoutSession;
        }
    }

    public class AddWorkoutSessionCommandHandler : IRequestHandler<AddWorkoutSessionCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddWorkoutSessionCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(AddWorkoutSessionCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(unitOfWork.AddWorkoutSession(request.workoutSession));
        }
    }
}

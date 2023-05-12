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
    public class DeleteWorkoutSessionCommand : IRequest<bool>
    {
        public WorkoutSession workoutSession { get; private set; }
        public DeleteWorkoutSessionCommand(WorkoutSession workoutSession)
        {
            this.workoutSession = workoutSession;
        }
    }

    public class DeleteWorkoutSessionCommandHandler : IRequestHandler<DeleteWorkoutSessionCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteWorkoutSessionCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteWorkoutSessionCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(unitOfWork.DeleteWorkoutSession(request.workoutSession));
        }
    }
}

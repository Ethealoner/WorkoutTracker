using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Application.Commands.Exercises
{
    public class DeleteExerciseCommand : IRequest<bool>
    {
        public Exercise exercise { get; private set; }
        public DeleteExerciseCommand(Exercise exercise)
        {
            this.exercise = exercise;
        }
    }

    public class DeleteExerciseCommanddHandler : IRequestHandler<DeleteExerciseCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteExerciseCommanddHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {
            unitOfWork.Exercises.Remove(request.exercise);

            return Task.FromResult(unitOfWork.Complete() > 0 ? true : false);
        }
    }
}

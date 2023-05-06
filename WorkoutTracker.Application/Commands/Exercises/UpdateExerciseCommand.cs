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
    public class UpdateExerciseCommand : IRequest<bool>
    {
        public Exercise exercise { get; private set; }
        public UpdateExerciseCommand(Exercise exercise)
        {
            this.exercise = exercise;
        }
    }

    public class UpdateExerciseCommandHandler : IRequestHandler<UpdateExerciseCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateExerciseCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
        {
            unitOfWork.Exercises.UpdateExercise(request.exercise);

            return Task.FromResult(unitOfWork.Complete() > 0 ? true : false);
        }
    }
}

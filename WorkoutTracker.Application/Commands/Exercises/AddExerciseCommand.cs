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
        public class AddExerciseCommand : IRequest<bool>
        {
            public Exercise exercise { get; private set; }
            public AddExerciseCommand(Exercise exercise)
            {
                this.exercise = exercise;
            }
        }

        public class AddExerciseCommandHandler : IRequestHandler<AddExerciseCommand, bool>
        {
            private readonly IUnitOfWork unitOfWork;

            public AddExerciseCommandHandler(IUnitOfWork unitOfWork)
            {
                this.unitOfWork = unitOfWork;
            }

            public Task<bool> Handle(AddExerciseCommand request, CancellationToken cancellationToken)
            {
                unitOfWork.Exercises.Add(request.exercise);

                return Task.FromResult(unitOfWork.Complete() > 0 ? true : false);
            }
        }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Application.Commands.ExerciseNotes
{
    public class AddExerciseNoteCommand : IRequest<bool>
    {
        public ExerciseNote ExerciseNote { get; set; }

        public AddExerciseNoteCommand(ExerciseNote exerciseNote)
        {
            ExerciseNote = exerciseNote;
        }
    }

    public class AddExerciseNoteCommandHandler : IRequestHandler<AddExerciseNoteCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddExerciseNoteCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(AddExerciseNoteCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(unitOfWork.AddOrUpdateExerciseNote(request.ExerciseNote));
        }
    }
}

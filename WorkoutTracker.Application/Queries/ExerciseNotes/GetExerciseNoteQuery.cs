using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Application.Queries.ExerciseNotes
{
    public class GetExerciseNoteQuery : IRequest<ExerciseNote>
    {
        public string ApplicationUserId { get; private set; }
        public string ExerciseName { get; private set; }

        public GetExerciseNoteQuery(string applicationUserId, string exerciseName)
        {
            ApplicationUserId = applicationUserId;
            ExerciseName = exerciseName;
        }
    }

    public class GetExerciseNoteQueryHandler : IRequestHandler<GetExerciseNoteQuery, ExerciseNote>
    {
        private readonly IExerciseNoteRepository ExerciseNoteRepository;

        public GetExerciseNoteQueryHandler(IExerciseNoteRepository exerciseNoteRepository)
        {
            ExerciseNoteRepository = exerciseNoteRepository;
        }

        public Task<ExerciseNote> Handle(GetExerciseNoteQuery request, CancellationToken cancellationToken)
        {
            var exerciseNote = ExerciseNoteRepository.Find(request.ApplicationUserId, request.ExerciseName);
            return Task.FromResult(exerciseNote);
        }
    }
}

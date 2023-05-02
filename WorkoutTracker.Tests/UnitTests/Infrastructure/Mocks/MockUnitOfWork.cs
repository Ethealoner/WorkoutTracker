using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTrackerMvc.Application.Interfaces;
using WorkoutTrackerMvc.Core.Models;

namespace WorkoutTracker.Tests.UnitTests.Infrastructure.Mocks
{
    internal class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetMock()
        {
            var mock = new Mock<IUnitOfWork>();
            var context = new Mock<DbContext>();
            context.Setup(x => x.Set<WorkoutSession>())
            var workoutSessionRepositoryMock = MockIWorkoutSessionRepository.GetMock();
            var exerciseRepositoryMock = MockIExerciseRepository.GetMock();

            mock.Setup(m => m.WorkoutSessions).Returns(() => workoutSessionRepositoryMock.Object);
            mock.Setup(m => m.Exercises).Returns(() => exerciseRepositoryMock.Object);
            mock.Setup(m => m.Complete()).Callback(() => { return; });

            return mock;
        }
    }
}

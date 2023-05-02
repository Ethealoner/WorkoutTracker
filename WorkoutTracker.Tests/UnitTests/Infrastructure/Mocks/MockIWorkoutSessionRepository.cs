using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Tests.UnitTests.Common.TestData;
using WorkoutTrackerMvc.Application.Interfaces;
using WorkoutTrackerMvc.Core.Models;

namespace WorkoutTracker.Tests.UnitTests.Infrastructure.Mocks
{
    internal class MockIWorkoutSessionRepository
    {
        public static Mock<IWorkoutSessionRepository> GetMock()
        {
            var mock = new Mock<IWorkoutSessionRepository>();

            mock.Setup(m => m.GetAll()).Returns(() => CommonTestData.workoutSessions);
            mock.Setup(m => m.Add(It.IsAny<WorkoutSession>())).Callback(() => { return; });

            return mock;
        }
    }
}

using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Tests.UnitTests.Common.TestData;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Tests.UnitTests.Common.Mocks
{
    internal class MockIWorkoutSessionRepository
    {
        public static Mock<IWorkoutSessionRepository> GetMock()
        {
            var mock = new Mock<IWorkoutSessionRepository>();

            mock.Setup(m => m.GetAll()).Returns(() => CommonTestData.workoutSessions);
            mock.Setup(m => m.GetById(It.IsAny<String>())).Returns((string id) => CommonTestData.workoutSessions.FirstOrDefault(o => o.WorkoutSessionId == id));
            mock.Setup(m => m.Add(It.IsAny<WorkoutSession>())).Callback(() => { return; });

            return mock;
        }
    }
}

using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Tests.UnitTests.Common.TestData;
using WorkoutTrackerMvc.Application.Interfaces;

namespace WorkoutTracker.Tests.UnitTests.Infrastructure.Mocks
{
    internal class MockIExerciseRepository
    {
        public static Mock<IExerciseRepository> GetMock()
        {
            var mock = new Mock<IExerciseRepository>();

            mock.Setup(m => m.GetAll()).Returns(() => CommonTestData.exercises);

            return mock;
        }
    }
}

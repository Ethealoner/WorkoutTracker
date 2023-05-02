using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Application.Queries.WorkoutSessions;
using WorkoutTracker.Infrastructure;
using WorkoutTracker.Tests.UnitTests.Common.Mocks;

namespace WorkoutTracker.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1Async()
        {
            IWorkoutSessionRepository mockRepository = MockIWorkoutSessionRepository.GetMock().Object;
            var handler = new GetWorkoutSessionByIdQueryHandler(mockRepository);
            var result = await handler.Handle(new GetWorkoutSessionByIdQuery("1"), CancellationToken.None);

            Assert.True(150.12 == result.WorkoutScore);
        }
    }
}
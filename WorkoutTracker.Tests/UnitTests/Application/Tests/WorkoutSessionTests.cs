using WorkoutTracker.Application.Commands.WorkoutSessions;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Application.Queries.WorkoutSessions;
using WorkoutTracker.Core.Models;
using WorkoutTracker.Infrastructure;
using WorkoutTracker.Tests.UnitTests.Common;
using WorkoutTracker.Tests.UnitTests.Common.Mocks;

namespace WorkoutTracker.Tests.UnitTests.Application.Tests
{
    public class WorkoutSessionTests
    {
        TestBase testbase;
        public WorkoutSessionTests()
        {
            testbase = new TestBase();
        }

        [Fact]
        public async Task GetOneWorkoutSession_WhenCalled_ReturnOneWorkoutSession()
        {
            WorkoutSessionRepository mockRepository = new WorkoutSessionRepository(testbase.ApplicationContext);
            var handler = new GetWorkoutSessionByIdQueryHandler(mockRepository);
            var result = await handler.Handle(new GetWorkoutSessionByIdQuery("1"), CancellationToken.None);

            Assert.True(150.12 == result.WorkoutScore);
        }

        [Fact]
        public async Task GetAllWorkoutSessions_WhenCalled_ReturnAllWorkoutSessions()
        {
            WorkoutSessionRepository mockRepository = new WorkoutSessionRepository(testbase.ApplicationContext);
            var handler = new GetAllWorkoutSessionsQueryHandler(mockRepository);
            var sessions = await handler.Handle(new GetAllWorkoutSessionsQuery(), CancellationToken.None);

            Assert.True(3 == sessions.Count());
        }

        [Fact]
        public async Task GetAllUsersWorkoutSessions_WhenCalled_ReturnAllUsersWorkoutSessions()
        {
            WorkoutSessionRepository mockRepository = new WorkoutSessionRepository(testbase.ApplicationContext);
            var handler = new GetWorkoutSessionsByUserIdQueryHandler(mockRepository);
            var result = await handler.Handle(new GetWorkoutSessionsByUserIdQuery("1"), CancellationToken.None);

            Assert.True(2 == result.Count());
        }

        [Fact]
        public async Task AddWorkoutSession_WhenCalled_WorkoutSessionAdded()
        {
            UnitOfWork unitOfWork = new UnitOfWork(testbase.ApplicationContext);
            var handler = new AddWorkoutSessionCommandHandler(unitOfWork);

            DateTime time = DateTime.Now;
            WorkoutSession session = new WorkoutSession("10", 0.0, time, "10");

            var result = await handler.Handle(new AddWorkoutSessionCommand(session), CancellationToken.None);

            var addedSession = unitOfWork.WorkoutSessions.GetById("10");

            Assert.True(result == true);

            Assert.NotNull(addedSession);
            Assert.True(0.0 == addedSession.WorkoutScore);
            Assert.True(time == addedSession.WorkoutDate);
        }
    }
}
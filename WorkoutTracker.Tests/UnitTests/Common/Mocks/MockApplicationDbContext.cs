using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Core.Models;
using WorkoutTracker.Infrastructure.Persistance;
using WorkoutTracker.Tests.UnitTests.Common.TestData;

namespace WorkoutTracker.Tests.UnitTests.Common.Mocks
{
    public static class MockApplicationDbContext
    {
        public static Mock<ApplicationDbContext> GetMockDbContext()
        {
            var mockWorkoutSessionSet = new Mock<DbSet<WorkoutSession>>();

            mockWorkoutSessionSet.As<IQueryable<WorkoutSession>>().Setup(e => e.Provider).Returns((CommonTestData.workoutSessions).AsQueryable().Provider);
            mockWorkoutSessionSet.As<IQueryable<WorkoutSession>>().Setup(e => e.Expression).Returns((CommonTestData.workoutSessions).AsQueryable().Expression);
            mockWorkoutSessionSet.As<IQueryable<WorkoutSession>>().Setup(e => e.ElementType).Returns((CommonTestData.workoutSessions).AsQueryable().ElementType);
            mockWorkoutSessionSet.As<IQueryable<WorkoutSession>>().Setup(e => e.GetEnumerator()).Returns((CommonTestData.workoutSessions).AsQueryable().GetEnumerator());

            var mockExercisesSet = new Mock<DbSet<Exercise>>();
            mockExercisesSet.As<IQueryable<Exercise>>().Setup(e => e.Provider).Returns((CommonTestData.exercises).AsQueryable().Provider);
            mockExercisesSet.As<IQueryable<Exercise>>().Setup(e => e.Expression).Returns((CommonTestData.exercises).AsQueryable().Expression);
            mockExercisesSet.As<IQueryable<Exercise>>().Setup(e => e.ElementType).Returns((CommonTestData.exercises).AsQueryable().ElementType);
            mockExercisesSet.As<IQueryable<Exercise>>().Setup(e => e.GetEnumerator()).Returns((CommonTestData.exercises).AsQueryable().GetEnumerator());

            var mockDbContext = new Mock<ApplicationDbContext>();
            mockDbContext.Setup(c => c.sessions).Returns(mockWorkoutSessionSet.Object);
            mockDbContext.Setup(c => c.exercises).Returns(mockExercisesSet.Object);

            return mockDbContext;
        }
    }
}

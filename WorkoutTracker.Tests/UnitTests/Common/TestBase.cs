using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Core.Models;
using WorkoutTracker.Infrastructure.Persistance;
using WorkoutTracker.Tests.UnitTests.Common.TestData;

namespace WorkoutTracker.Tests.UnitTests.Common
{
    public class TestBase : IDisposable
    {
        public ApplicationDbContext ApplicationContext { get; private set; }

        public TestBase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ApplicationDbContext" + Guid.NewGuid().ToString())
                .Options;

            ApplicationContext = new ApplicationDbContext(options);
            ApplicationContext.Database.EnsureDeleted();
            ApplicationContext.Database.EnsureCreated();

            ApplicationContext.Set<Exercise>().AddRange(CommonTestData.exercises);
            ApplicationContext.Set<WorkoutSession>().AddRange(CommonTestData.workoutSessions);
            ApplicationContext.SaveChanges();
        }

        public void Dispose()
        {
            ApplicationContext.Dispose();
        }
    }
}

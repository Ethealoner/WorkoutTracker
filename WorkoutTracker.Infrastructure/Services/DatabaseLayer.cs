using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Infrastructure.Repositories;

namespace WorkoutTracker.Infrastructure.Services
{
    public static class DatabaseLayer
    {
        public static void AddDatabaseLayer(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IWorkoutSessionRepository, WorkoutSessionRepository>();
            services.AddTransient<IExerciseRepository, ExerciseRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}

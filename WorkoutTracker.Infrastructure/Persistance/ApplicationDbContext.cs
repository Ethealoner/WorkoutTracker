using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Core.Models;
using WorkoutTracker.Infrastructure.Identity;

namespace WorkoutTracker.Infrastructure.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<WorkoutSession> sessions { get; set; }
        public DbSet<Exercise> exercises { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

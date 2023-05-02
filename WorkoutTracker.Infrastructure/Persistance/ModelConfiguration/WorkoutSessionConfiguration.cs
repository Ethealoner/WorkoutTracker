using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Core.Models;
using WorkoutTracker.Infrastructure.Identity;

namespace WorkoutTracker.Infrastructure.Persistance.ModelConfiguration
{
    public class WorkoutSessionConfiguration : IEntityTypeConfiguration<WorkoutSession>
    {
        public void Configure(EntityTypeBuilder<WorkoutSession> builder)
        {
            builder.HasKey(s => s.WorkoutSessionId);

            builder
                .ToTable("WorkoutSessions");

            builder
                .Property(s => s.WorkoutSessionId)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(typeof(ApplicationUser))
                .WithMany("WorkoutSessions")
                .HasForeignKey("ApplicationUserId")
                .IsRequired();

            builder
                .Property(s => s.WorkoutScore)
                .HasDefaultValue(0)
                .HasPrecision(2)
                .IsRequired();

            builder
                .Property(s => s.WorkoutDate)
                .IsRequired();

            builder
                .HasMany(s => s.Exercise)
                .WithOne(e => e.WorkoutSession)
                .HasForeignKey(e => e.WorkoutSessionId); 
        }
    }
}

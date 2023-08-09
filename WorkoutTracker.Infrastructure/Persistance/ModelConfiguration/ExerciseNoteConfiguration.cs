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
    public class ExerciseNoteConfiguration : IEntityTypeConfiguration<ExerciseNote>
    {
        public void Configure(EntityTypeBuilder<ExerciseNote> builder)
        {
            builder
                .ToTable("ExerciseNote");

            builder
                .HasKey(e => new { e.ApplicationUserId, e.ExerciseName });

            builder
                .HasOne(typeof(ApplicationUser))
                .WithMany("ExerciseNotes")
                .HasForeignKey("ApplicationUserId")
                .IsRequired();

            builder
                .Property(e => e.Notes)
                .HasMaxLength(1000)
                .IsRequired();

            builder
                .Property(e => e.ExerciseName)
                .HasMaxLength(30);
        }
    }
}

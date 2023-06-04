using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Infrastructure.Persistance.ModelConfiguration
{
    public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            // Composite Key
            builder
                .HasKey(e => new {e.ExerciseId, e.WorkoutSessionId});

            builder
                .Property(e => e.ExerciseId)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .ToTable("Exercises");

            builder
                .HasOne(e => e.WorkoutSession)
                .WithMany(s => s.Exercise)
                .HasForeignKey(e => e.WorkoutSessionId);

            builder
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .Property(e => e.Sets)
                .HasMaxLength(1000)
                .HasConversion(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<ICollection<Set>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));

            builder
                .Property(e => e.ExerciseScore)
                .HasDefaultValue(0)
                .HasPrecision(2);
        }
    }
}

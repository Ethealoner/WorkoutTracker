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
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(a => a.Id)
                .HasColumnName("ApplicationUserId");
        }
    }
}

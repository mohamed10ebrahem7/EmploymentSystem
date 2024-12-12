using EmploymentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Infrastructure.Persistence.Configurations;

public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
{
    public void Configure(EntityTypeBuilder<Vacancy> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(v => v.Description)
            .IsRequired();

        builder.Property(v => v.MaxApplications)
            .IsRequired();

        builder.Property(v => v.ExpiryDate)
            .IsRequired();

        builder.Property(v => v.IsActive)
            .HasDefaultValue(true);

        //builder.Property(v => v.IsArchived)
        //    .HasDefaultValue(false);

        builder.HasMany(v => v.Applications)
            .WithOne(a => a.Vacancy)
            .HasForeignKey(a => a.VacancyId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete on applications
    }
}


using EmploymentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Infrastructure.Persistence.Configurations;

public class ApplicationConfiguration: IEntityTypeConfiguration<Domain.Entities.Application>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Application> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.AppliedDate)
            .IsRequired();

        builder.HasOne(a => a.Applicant)
            .WithMany(ap => ap.Applications)
            .HasForeignKey(a => a.ApplicantId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

        builder.HasOne(a => a.Vacancy)
            .WithMany(v => v.Applications)
            .HasForeignKey(a => a.VacancyId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
    }
}

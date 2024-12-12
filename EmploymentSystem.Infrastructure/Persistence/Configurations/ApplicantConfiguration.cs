using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Infrastructure.Persistence.Configurations;
public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.IdentityUserId)
            .IsRequired();

        builder.HasIndex(e => e.IdentityUserId)
            .IsUnique();

        builder.HasMany(a => a.Applications)
            .WithOne(ap => ap.Applicant)
            .HasForeignKey(ap => ap.ApplicantId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}


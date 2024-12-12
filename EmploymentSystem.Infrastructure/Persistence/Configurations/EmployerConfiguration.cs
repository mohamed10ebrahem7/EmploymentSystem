using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Infrastructure.Persistence.Configurations;

public class EmployerConfiguration : IEntityTypeConfiguration<Employer>
{
    public void Configure(EntityTypeBuilder<Employer> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.IdentityUserId)
            .IsRequired();

        builder.HasIndex(e => e.IdentityUserId)
            .IsUnique();

        builder.HasMany(e => e.Vacancies)
            .WithOne(v => v.Employer)
            .HasForeignKey(v => v.EmployerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}


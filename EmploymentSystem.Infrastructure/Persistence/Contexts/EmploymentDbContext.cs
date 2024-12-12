using CleanArchitecture.Infrastructure.Identity;
using EmploymentSystem.Application.Abstractions;
using EmploymentSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Infrastructure.Persistence.Contexts
{
    public class EmploymentDbContext : IdentityDbContext<ApplicationUser>, IEmploymentDbContext
    {
        public EmploymentDbContext(DbContextOptions<EmploymentDbContext> options) : base(options) { }

        public DbSet<Employer> Employers { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Domain.Entities.Application> Applications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmploymentDbContext).Assembly);
        }
    }
}

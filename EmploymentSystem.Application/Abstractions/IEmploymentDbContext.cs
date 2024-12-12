using EmploymentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Abstractions
{
    public interface IEmploymentDbContext
    {
        DbSet<Employer> Employers { get; set; }
        DbSet<Applicant> Applicants { get; set; }
        DbSet<Vacancy> Vacancies { get; set; }
        DbSet<Domain.Entities.Application> Applications { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

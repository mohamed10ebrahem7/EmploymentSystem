using EmploymentSystem.Application.Dtos.Request;
using EmploymentSystem.Application.Infrastructure;
using EmploymentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Abstractions;
public interface IVacancyRepository
{
    Task<DefaultResult> AddVacancy(VacancyDto info);
    Task<DefaultResult> UpdateVacancy(VacancyDto info);
    Task<DefaultResult> DeleteVacancy(int Id);
    Task<DefaultResult> GetVacancyById(int Id);
    Task<DefaultResult> GetVacancyByName(string Name);
    Task<DefaultResult> GetAllVacancies();
    Task<Vacancy> GetVacancyWithApplications(int id);
}

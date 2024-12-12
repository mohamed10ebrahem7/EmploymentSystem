using Azure.Core;
using EmploymentSystem.Application.Abstractions;
using EmploymentSystem.Application.Dtos.Request;
using EmploymentSystem.Application.Infrastructure;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Infrastructure.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmploymentSystem.Infrastructure.Persistence.Repositories;
public class VacancyRepository : IVacancyRepository
{
    IEmploymentDbContext _employmentDbContext;
    public VacancyRepository(IEmploymentDbContext employmentDbContext)
    {
        _employmentDbContext = employmentDbContext;
    }


    public async Task<DefaultResult> AddVacancy(VacancyDto info)
    {
        try
        {

            var vacancy = new Vacancy
            {
                Title = info.Title,
                Description = info.Description,
                MaxApplications = info.MaxApplications,
                ExpiryDate = info.ExpiryDate,
                IsActive = true,
                IsArchived = false,
                EmployerId = info.EmployerId
            };

            _employmentDbContext.Vacancies.Add(vacancy);
            var result = await _employmentDbContext.SaveChangesAsync();

            return new DefaultResult
            {
                result = result,
                errorOccured = false
            };
        }
        catch (Exception ex)
        {
            return new DefaultResult
            {
                errorOccured = true,
                errorMessage = "Unable to create vacancy"
            };
        }


    }

    public async Task<DefaultResult> UpdateVacancy(VacancyDto info)
    {
        try
        {
            var vacancy = await _employmentDbContext.Vacancies.FindAsync(info.Id);
            if (vacancy == null) return new DefaultResult { errorOccured = true, errorMessage = "Unable To find the vacancy" };
            if(!info.Title.IsNullOrEmpty())
                vacancy.Title = info.Title;
            if (!info.Description.IsNullOrEmpty())
                vacancy.Description = info.Description;
            if (info.MaxApplications > 0)
                vacancy.MaxApplications = info.MaxApplications;
            if (info.ExpiryDate > DateTime.MinValue)
                vacancy.ExpiryDate = info.ExpiryDate;

            vacancy.IsActive = info.IsActive;

            var result = await _employmentDbContext.SaveChangesAsync();
            return new DefaultResult
            {
                result = result,
                errorOccured = false
            };
        }
        catch
        {
            return new DefaultResult
            {
                errorOccured = true,
                errorMessage = "Unable to update vacancy"
            };

        }
    }
    public async Task<DefaultResult> DeleteVacancy(int Id)
    {
        try
        {
            var vacancy = await _employmentDbContext.Vacancies.FindAsync(Id);
            if (vacancy == null) return new DefaultResult { errorOccured = true, errorMessage = "Unable To find the vacancy" }; ;

            _employmentDbContext.Vacancies.Remove(vacancy);
            var result = await _employmentDbContext.SaveChangesAsync();

            return new DefaultResult
            {
                result = result,
                errorOccured = false
            };
        }
        catch
        {
            return new DefaultResult
            {
                errorOccured = true,
                errorMessage = "Unable to Delete vacancy"
            };
        }
    }
    public async Task<DefaultResult> GetVacancyById(int Id)
    {
        try
        {
            //var vacancy = await _employmentDbContext.Vacancies.FindAsync(Id);
            var vacancy = await _employmentDbContext.Vacancies.FirstOrDefaultAsync(v => v.Id == Id && v.ExpiryDate >= DateTime.Now);
            if (vacancy == null) new DefaultResult { errorOccured = true, errorMessage = "Unable To find the vacancy" }; ;

            var result = new VacancyDto
            {
                Id = vacancy.Id,
                Title = vacancy.Title,
                Description = vacancy.Description,
                MaxApplications = vacancy.MaxApplications,
                ExpiryDate = vacancy.ExpiryDate,
                IsActive = vacancy.IsActive,
                EmployerId = vacancy.EmployerId
            };
            return new DefaultResult
            {
                result = result,
                errorOccured = false
            };
        }
        catch
        {
            return new DefaultResult
            {
                errorOccured = true,
                errorMessage = "Unable to Find the vacancy"
            };
        }
    }
    public async Task<DefaultResult> GetVacancyByName(string Name)
    {
        try
        {
            //var vacancy = await _employmentDbContext.Vacancies.FindAsync(Id);
            var vacancies = await _employmentDbContext.Vacancies.Where(v => v.Title.Contains(Name) && v.ExpiryDate >= DateTime.Now).ToListAsync();
            if (vacancies == null) new DefaultResult { errorOccured = true, errorMessage = "Unable To find the vacancy" }; ;

            var result = vacancies.Select(v => new VacancyDto
            {
                Id = v.Id,
                Title = v.Title,
                Description = v.Description,
                MaxApplications = v.MaxApplications,
                ExpiryDate = v.ExpiryDate,
                IsActive = v.IsActive,
                EmployerId = v.EmployerId
            }).ToList();
            return new DefaultResult
            {
                result = result,
                errorOccured = false
            };
        }
        catch
        {
            return new DefaultResult
            {
                errorOccured = true,
                errorMessage = "Unable to Find the vacancy"
            };
        }
    }
    public async Task<DefaultResult> GetAllVacancies()
    {
        try
        {
            var vacancies = await _employmentDbContext.Vacancies.Where(v => v.ExpiryDate >= DateTime.Now).ToListAsync();

            var result = vacancies.Select(v => new VacancyDto
            {
                Id = v.Id,
                Title = v.Title,
                Description = v.Description,
                MaxApplications = v.MaxApplications,
                ExpiryDate = v.ExpiryDate,
                IsActive = v.IsActive,
                EmployerId = v.EmployerId
            }).ToList();
            return new DefaultResult
            {
                result = result,
                errorOccured = false
            };
        }
        catch
        {
            return new DefaultResult
            {
                errorOccured = true,
                errorMessage = "Unable to Get the vacancies"
            };
        }
    }
    public async Task<Vacancy> GetVacancyWithApplications(int vacancyId)
    {
        try
        {
            var vacancy = await _employmentDbContext.Vacancies
            .Include(v => v.Applications)
            .FirstOrDefaultAsync(v => v.Id == vacancyId);

            if (vacancy == null)
                return new Vacancy();//new DefaultResult {errorOccured = true, errorMessage= "There is no applications assinesd to the vacancy" };

            return vacancy;
        }
        catch
        {
            return new Vacancy();
        }
    }
}
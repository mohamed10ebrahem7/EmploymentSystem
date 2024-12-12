using Azure.Core;
using EmploymentSystem.Application.Abstractions;
using EmploymentSystem.Application.Dtos.Request;
using EmploymentSystem.Application.Infrastructure;
using EmploymentSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmploymentSystem.Infrastructure.Persistence.Repositories;
public class ApplicationRepository : IApplicationRepository
{

    IEmploymentDbContext _employmentDbContext;
    IVacancyRepository _vacancyRepository;
    IMemoryCache _cache;

    public ApplicationRepository(IEmploymentDbContext employmentDbContext, IVacancyRepository vacancyRepository, IMemoryCache cache)
    {
        _employmentDbContext = employmentDbContext;
        _vacancyRepository = vacancyRepository;
        _cache = cache;
    }

    public async Task<DefaultResult> AddApplication(ApplicationDto info)
    {
        try
        {
            string CacheKey = info.ApplicantId.ToString();
            
            if (!_cache.TryGetValue(CacheKey, out DateTime CashedAppliedDate))
            {
                var currentTime = DateTime.UtcNow;

                var recentApplication = await _employmentDbContext.Applications
                    .Where(a => a.ApplicantId == info.ApplicantId
                                && a.AppliedDate <= currentTime.AddHours(+24))
                    .FirstOrDefaultAsync();

                if (recentApplication != null)
                {
                    // Cache
                    _cache.Set(CacheKey, recentApplication.AppliedDate, TimeSpan.FromHours(24));
                    return new DefaultResult { errorOccured = true, errorMessage = "Each applicant can only apply for one vacancy per day." };
                }

            }
            else
            {
                if (CashedAppliedDate > DateTime.UtcNow.AddHours(+24))
                {
                    _cache.Remove(CacheKey);
                }
                else
                {
                    return new DefaultResult { errorOccured = true, errorMessage = "Each applicant can only apply for one vacancy per day." };

                }
            }



            var vacancyApplications = await _vacancyRepository.GetVacancyWithApplications(info.VacancyId);
            if (vacancyApplications.Applications.Count >= vacancyApplications.MaxApplications)
                    return new DefaultResult { errorOccured = true, errorMessage = "The maximum number of applications for this vacancy has been reached." };

            var application = new Domain.Entities.Application
            {
                AppliedDate = DateTime.UtcNow,
                ApplicantId = info.ApplicantId,
                VacancyId = info.VacancyId
            };

            _employmentDbContext.Applications.Add(application);
            await _employmentDbContext.SaveChangesAsync();

            _cache.Set(CacheKey, application.AppliedDate, TimeSpan.FromHours(24));
            return new DefaultResult
            {
                result = application.Id,
                errorOccured = false
            };
        }
        catch (Exception ex)
        {
            return new DefaultResult
            {
                errorOccured = true,
                errorMessage = "Unable to create Application"
            };
        }

    }
}

using EmploymentSystem.Application.Abstractions;
using EmploymentSystem.Application.Dtos.Request;
using EmploymentSystem.Application.Features.Vacancy.Commands;
using EmploymentSystem.Application.Features.Vacancy.Queries;
using EmploymentSystem.Application.Features.Vacancy.Queriesl;
using EmploymentSystem.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Vacancy;
public class VacancyHandler :
    IRequestHandler<CreateVacancyCommand, DefaultResult>,
    IRequestHandler<UpdateVacancyCommand, DefaultResult>,
    IRequestHandler<DeleteVacancyCommand, DefaultResult>,
    IRequestHandler<GetVacancyByIdQuery, DefaultResult>,
    IRequestHandler<GetAllVacanciesQuery, DefaultResult>,
    IRequestHandler<GetApplicantsForVacancyQuery, DefaultResult>,
    IRequestHandler<GetVacancyByNameQuery, DefaultResult>
{
    private readonly IVacancyRepository _vacancyRepository;

    public VacancyHandler(IVacancyRepository vacancyRepository)
    {
        _vacancyRepository = vacancyRepository;
    }

    public async Task<DefaultResult> Handle(CreateVacancyCommand request, CancellationToken cancellationToken)
    {
        return await _vacancyRepository.AddVacancy(request.info);
    }

    public async Task<DefaultResult> Handle(UpdateVacancyCommand request, CancellationToken cancellationToken)
    {
        return await _vacancyRepository.UpdateVacancy(request.info);
    }

    public async Task<DefaultResult> Handle(DeleteVacancyCommand request, CancellationToken cancellationToken)
    {
        return await _vacancyRepository.DeleteVacancy(request.Id);
    }

    public async Task<DefaultResult> Handle(GetVacancyByIdQuery request, CancellationToken cancellationToken)
    {
        return await _vacancyRepository.GetVacancyById(request.Id);
    }
    public async Task<DefaultResult> Handle(GetAllVacanciesQuery request, CancellationToken cancellationToken)
    {
        return await _vacancyRepository.GetAllVacancies();
    }
    public async Task<DefaultResult> Handle(GetApplicantsForVacancyQuery request, CancellationToken cancellationToken)
    {
        var response = await _vacancyRepository.GetVacancyWithApplications(request.Id);
        return response == null ? default(DefaultResult) : new DefaultResult { errorOccured = false, result = response };
    }
    public async Task<DefaultResult> Handle(GetVacancyByNameQuery request, CancellationToken cancellationToken)
    {
        return await _vacancyRepository.GetVacancyByName(request.Name);
    }
}

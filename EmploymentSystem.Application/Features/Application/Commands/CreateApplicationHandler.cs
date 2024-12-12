using EmploymentSystem.Application.Abstractions;
using EmploymentSystem.Application.Infrastructure;
using MediatR;

namespace EmploymentSystem.Application.Features.Application.Commands;

public class CreateApplicationHandler : IRequestHandler<CreateApplicationCommand, DefaultResult>
{
    private readonly IApplicationRepository _applicationRepository;

    public CreateApplicationHandler(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<DefaultResult> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {

        return await _applicationRepository.AddApplication(request.Application);
    }
}

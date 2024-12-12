using EmploymentSystem.Application.Dtos.Request;
using EmploymentSystem.Application.Infrastructure;
using MediatR;

namespace EmploymentSystem.Application.Features.Application.Commands;

public class CreateApplicationCommand : IRequest<DefaultResult>
{
    public ApplicationDto Application { get; set; }
}

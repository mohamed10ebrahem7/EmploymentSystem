using EmploymentSystem.Application.Dtos.Request;
using EmploymentSystem.Application.Infrastructure;
using MediatR;

namespace EmploymentSystem.Application.Features.Login;

public class LoginUserCommand : IRequest<DefaultResult>
{
    public LoginReqDto info { get; set; }
}


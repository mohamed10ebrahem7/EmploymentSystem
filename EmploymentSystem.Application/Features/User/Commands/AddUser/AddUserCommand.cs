using EmploymentSystem.Application.Dtos.Request;
using EmploymentSystem.Application.Infrastructure;
using MediatR;

namespace EmploymentSystem.Application.Features.User.Commands.AddUser;

public class AddUserCommand : IRequest<DefaultResult>
{
    public UserInfoReqDto info { get; set; }
}


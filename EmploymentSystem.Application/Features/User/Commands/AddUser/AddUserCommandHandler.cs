using EmploymentSystem.Application.Abstractions;
using EmploymentSystem.Application.Infrastructure;
using MediatR;

namespace EmploymentSystem.Application.Features.User.Commands.AddUser;
public class AddUserCommandHandler : IRequestHandler<AddUserCommand, DefaultResult>
{
    private readonly IIdentityService _identityService;
    public AddUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    public async Task<DefaultResult> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.CreateUserAsync(request.info);
    }
}

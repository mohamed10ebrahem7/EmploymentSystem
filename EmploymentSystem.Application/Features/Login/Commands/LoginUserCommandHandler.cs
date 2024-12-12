using EmploymentSystem.Application.Abstractions;
using EmploymentSystem.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Login.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, DefaultResult>
    {
        private readonly IIdentityService _identityService;

        public LoginUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<DefaultResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.signIn(request.info);
        }
    }
}

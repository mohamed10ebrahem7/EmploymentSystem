using EmploymentSystem.Application.Dtos.Response.Login;
using EmploymentSystem.Application.Features.Login;
using EmploymentSystem.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Abstractions
{
    public interface IJwtTokenService
    {
        DefaultResult Create(LoginResDto user);
    }
}

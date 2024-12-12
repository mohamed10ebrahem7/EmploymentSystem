using EmploymentSystem.Application.Dtos.Request;
using EmploymentSystem.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Abstractions;
public interface IIdentityService
{
    Task<DefaultResult> CreateUserAsync(UserInfoReqDto userData);
    Task<bool> RoleExistsAsync(string role);
    Task<DefaultResult> signIn(LoginReqDto info);
}


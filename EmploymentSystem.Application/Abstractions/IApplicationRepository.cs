using EmploymentSystem.Application.Dtos.Request;
using EmploymentSystem.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Abstractions;
public interface IApplicationRepository
{
    Task<DefaultResult> AddApplication(ApplicationDto info);
}

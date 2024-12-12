using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Infrastructure;

public class DefaultResult
{
    public object result { get; set; }
    public bool errorOccured { get; set; }
    public string errorMessage { get; set; }
    public List<string> errorMessages { get; set; }
    public int StatusCode { get; set; }
}

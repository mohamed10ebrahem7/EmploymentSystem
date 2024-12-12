using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Dtos.Request
{
    public class LoginReqDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

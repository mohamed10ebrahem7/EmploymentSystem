using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Dtos.Response.Login
{
    public class LoginResDto
    {
        public string id { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public bool emailConfirmed { get; set; }
        public string role { get; set; }

        public string fullName { set; get; }
        public string jobTitle { get; set; }

    }
}

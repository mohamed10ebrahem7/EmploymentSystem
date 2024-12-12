using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public string JobTitle { get; set; }
}

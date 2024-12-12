using Azure.Core;
using EmploymentSystem.Application.Abstractions;
using EmploymentSystem.Application.Dtos.Request;
using EmploymentSystem.Application.Dtos.Response.Login;
using EmploymentSystem.Application.Infrastructure;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Infrastructure.Persistence.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CleanArchitecture.Infrastructure.Identity;

public class IdentityService : IIdentityService
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IEmploymentDbContext _employmentDbContext;

    public IdentityService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager
        , SignInManager<ApplicationUser> signInManager, IJwtTokenService jwtTokenService, IEmploymentDbContext employmentDbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
        _employmentDbContext = employmentDbContext;

    }

    public async Task<DefaultResult> CreateUserAsync(UserInfoReqDto userData)
    {
        try
        {


            var user = new ApplicationUser
            {
                FullName = userData.FullName,
                JobTitle = userData.JobTitle,
                Email = userData.Email,
                UserName = userData.Email,
                PhoneNumber = userData.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, userData.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(userData.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(userData.Role));
                }
                await _userManager.AddToRoleAsync(user, userData.Role);
                if (userData.Role == "Employer")
                {
                    var emplyer = new Employer
                    {
                        IdentityUserId = user.Id,
                    };
                    _employmentDbContext.Employers.Add(emplyer);
                    await _employmentDbContext.SaveChangesAsync();
                }
                else if (userData.Role == "Applicant")
                {
                    var applicant = new Applicant
                    {
                        IdentityUserId = user.Id,
                    };
                    _employmentDbContext.Applicants.Add(applicant);
                    await _employmentDbContext.SaveChangesAsync();
                }
                return new DefaultResult
                {
                    errorOccured = false,
                    result = Guid.Parse(user.Id)
                };
            }
            var errorMessages = result.Errors.Select(e => e.Description).ToList();
            return new DefaultResult
            {
                errorOccured = true,
                errorMessages = errorMessages
            };
        }
        catch (Exception ex)
        {
            return new DefaultResult
            {
                errorOccured = true,
                errorMessage = "Can't create user",
                StatusCode = 500
            };
        }
    }
    public async Task<bool> RoleExistsAsync(string role)
    {
        return await _roleManager.RoleExistsAsync(role);
    }

    public async Task<DefaultResult> signIn(LoginReqDto info)
    {
        try
        {

            var result = await _signInManager.PasswordSignInAsync(info.Email, info.Password, false, false);
            string errorMessage = "Auth failed";
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(info.Email);
                var roles = await _userManager.GetRolesAsync(user);
                var LoginResponse = new LoginResDto
                {
                    id = user.Id,
                    phoneNumber = user.PhoneNumber,
                    email = user.Email,
                    emailConfirmed = user.EmailConfirmed,
                    fullName = user.FullName,
                    jobTitle = user.JobTitle,
                    role = string.Join(",", roles)
                };
                var token = _jwtTokenService.Create(LoginResponse);
                if (token.errorOccured == false)
                    return token;
                else
                    errorMessage = token.errorMessage;
            }

            return new DefaultResult
            {
                errorOccured = true,
                errorMessage = errorMessage
            };
        }
        catch (Exception ex)
        {
            return new DefaultResult
            {
                errorOccured = true,
                errorMessage = "Auth failed"
            };
        }
    }


}

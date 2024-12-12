

using CleanArchitecture.Infrastructure.Identity;
using EmploymentSystem.Application.Abstractions;
using EmploymentSystem.Infrastructure.Persistence.Contexts;
using EmploymentSystem.Infrastructure.Persistence.Identity;
using EmploymentSystem.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmploymentSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<IEmploymentDbContext, EmploymentDbContext>(options =>
        options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddSingleton(TimeProvider.System);
        services.AddScoped<IIdentityService, IdentityService>();

        services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<EmploymentDbContext>()
        .AddDefaultTokenProviders();

        services.AddIdentityCore<ApplicationUser>(options =>
        {
        }).AddEntityFrameworkStores<EmploymentDbContext>()
        .AddSignInManager();


        services.AddSingleton<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IApplicationRepository , ApplicationRepository>();
        services.AddScoped<IVacancyRepository, VacancyRepository>();
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ApplicantPolicy", policy => policy.RequireRole("Applicant"));
            options.AddPolicy("EmployerPolicy", policy => policy.RequireRole("Employer"));
        });
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
                    .AddJwtBearer(options =>
                    {
                        options.SaveToken = true;
                        options.RequireHttpsMetadata = false;

                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidAudience = config["Jwt:Audience"],
                            ValidIssuer = config["Jwt:Issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Secret"]))
                        };
                    });
        services.AddMemoryCache();
        return services;
    }
}

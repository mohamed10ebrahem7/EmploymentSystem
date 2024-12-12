using Azure.Core;
using EmploymentSystem.Application.Abstractions;
using EmploymentSystem.Application.Dtos.Response.Login;
using EmploymentSystem.Application.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Infrastructure.Persistence.Identity;
public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public DefaultResult Create(LoginResDto user)
    {
        try
        {

            string secretKey = _configuration["Jwt:Secret"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                      new Claim(JwtRegisteredClaimNames.Sub, user.id.ToString()),
                      new Claim(JwtRegisteredClaimNames.Email, user.email.ToString()),
                      new Claim(ClaimTypes.Role, user.role)
                    }),
                Expires = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
                SigningCredentials = credentials,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
            };

            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new DefaultResult
            {
                result = accessToken,
                errorOccured = false
            };
        }
        catch (Exception ex)
        {
            return new DefaultResult
            {
                errorOccured = true,
                errorMessage = "Can't generate Token"
            };
        }

    }
}

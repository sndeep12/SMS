using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Api.Controllers;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Infrastructure.Persistence;
using SchoolManagement.Infrastructure.Repositories;
using SchoolManagement.Infrastructure.Services;
using System.Text;

namespace SchoolManagement.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseMySql(
                config.GetConnectionString("Default"),
                ServerVersion.AutoDetect(config.GetConnectionString("Default"))
               
            ).LogTo(Console.WriteLine, LogLevel.Information); ;
        });
    

        // Repositories
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        // Services
        services.AddScoped<AuthService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<SchoolService>();
        services.AddScoped<AcademicYearService>();
        services.AddScoped<ClassService>();
        services.AddScoped<MediumService>();
        services.AddScoped<SectionService>();
        services.AddScoped<SubjectService>();
        services.AddScoped<SubjectMappingService>();
        services.AddScoped<SectionMappingService>();

        var jwtKey = config["Jwt:Key"] ?? "CHANGE_ME";
        var issuer = config["Jwt:Issuer"];
        var audience = config["Jwt:Audience"];

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });

        

        return services;
    }
}

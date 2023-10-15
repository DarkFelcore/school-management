using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Infrastructure.Authentication;
using SchoolManagement.Infrastructure.Identity;
using SchoolManagement.Infrastructure.Persistance;
using SchoolManagement.Infrastructure.Persistance.Repositories;
using SchoolManagement.Infrastructure.Services;

namespace SchoolManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuth(configuration)
                .AddPersistance();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            return services;
        }

        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer("server=localhost\\sqlexpress;database=SchoolManagement;User Id=sa;Password=password123!;trusted_connection=true;TrustServerCertificate=true;MultipleActiveResultSets=True")
            );
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            // dotnet user-secrets init --project .\BuberDinner.Api\
            // dotnet user-secrets set  --project .\BuberDinner.Api\ "JwtSettings:Secret" "super-secret-key-from-user-secrets"
            // dotnet user-secrets list --project .\BuberDinner.Api\

            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,

                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret)
                    )
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(IdentityData.AdminUserPolicyName, pb =>
                {
                    pb.RequireClaim(IdentityData.AdminUserClaimName, "true");
                });
            });

            return services;
        }
    }
}

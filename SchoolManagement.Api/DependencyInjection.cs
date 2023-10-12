using Microsoft.AspNetCore.Mvc.Infrastructure;
using SchoolManagement.Api.Common;
using SchoolManagement.Api.Common.Errors;

namespace SchoolManagement.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, SchoolManagementProblemDetailFactory>();
            services.AddMappings();
            return services;
        }
    }
}

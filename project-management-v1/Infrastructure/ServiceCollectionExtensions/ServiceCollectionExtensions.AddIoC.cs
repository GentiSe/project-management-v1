using project_management_v1.Application.Repository;
using project_management_v1.Infrastructure.Data;
using project_management_v1.Infrastructure.Services;

namespace project_management_v1.Infrastructure.ServiceCollectionExtensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder AddIoC(this WebApplicationBuilder builder)
        {
            builder.Services.AddIoC();
            return builder;
        }

        public static IServiceCollection AddIoC(this IServiceCollection services)
        {
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IProjectManagementRepository, ProjectManagementRepository>();
            services.AddScoped<ISalesDataService, SalesDataService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IJsonSerializerService, JsonSerializerService>();
            return services;
        }
    }
}

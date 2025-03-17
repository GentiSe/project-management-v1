using project_management_v1.Application.Domain.Constants;

namespace project_management_v1.Infrastructure.ServiceCollectionExtensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder UseAuthorization(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization();
            return builder;
        }

        public static IServiceCollection AddAuthorization(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy("CanRead", policy =>
                {
                    policy.RequireRole(UserRoles.Analyst, UserRoles.Admin, UserRoles.Basic);
                })
                .AddPolicy("CanWrite", policy =>
                {
                    policy.RequireRole(UserRoles.Admin);
                });

            return services;
        }
    }
}

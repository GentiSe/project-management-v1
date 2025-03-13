using Microsoft.AspNetCore.Identity;
using project_management_v1.Infrastructure.Data;

namespace project_management_v1.Infrastructure.ServiceCollectionExtensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder AddIdentityService(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentityService();
            return builder;
        }

        public static IServiceCollection AddIdentityService(this  IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}

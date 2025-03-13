using Microsoft.EntityFrameworkCore;
using project_management_v1.Infrastructure.Data;

namespace project_management_v1.Infrastructure.ServiceCollectionExtensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext(builder.Configuration);
            return builder;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services
            , IConfiguration configuration)
        {
            services
                .AddDbContext<ApplicationDbContext>(options => 
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }           
    }
}

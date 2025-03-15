using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace project_management_v1.Infrastructure.ServiceCollectionExtensions
{
    public static partial class ServiceCollectionExtensionsAddAuth
    {
        public static WebApplicationBuilder UseAuth(this  WebApplicationBuilder builder)
        {
            builder.Services.AddAuth();
            return builder;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = false,
                        ValidIssuer = "https://localhost:7171",
                        ValidAudience = "https://localhost:7171",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("5ee239b2-82cd-45cb-974e-65d3b51ee7db"))
                    };
                });

            return services;
        }
    }
}

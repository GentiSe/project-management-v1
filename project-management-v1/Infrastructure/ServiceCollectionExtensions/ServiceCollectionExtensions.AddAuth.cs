using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace project_management_v1.Infrastructure.ServiceCollectionExtensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder UseAuth(this  WebApplicationBuilder builder)
        {
            builder.Services.AddAuth(builder.Configuration);
            return builder;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(cfg => {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => {
                var secret = configuration.GetValue<string>("ApplicationSettings:JWT_Secret");

                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8
                        .GetBytes(secret)
                    ),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}

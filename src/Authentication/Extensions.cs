using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Outloud.Common;

namespace Outloud.Common.Authentication
{
    public static class Extensions
    {
        public static IServiceCollection AddAuth0(this IServiceCollection services)
        {
            Auth0Options options;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                services.Configure<Auth0Options>(configuration.GetSection("auth0"));
                options = configuration.GetOptions<Auth0Options>("auth0");
            }
            services.AddAuthentication(configuration =>
            {
                configuration.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                configuration.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configuration =>
            {
                configuration.Authority = options.Authority;
                configuration.Audience = options.Audience;
                configuration.RequireHttpsMetadata = options.RequireHttps;
            });

            services.AddAuthorization(configuration => {
                configuration.AddPolicy(Policies.RequireAdmin, policy => policy.RequireClaim(options.Claim, "admin"));
            });

            return services;
        }

        public static IApplicationBuilder UseAuth0(this IApplicationBuilder builder)
        {
            builder.UseAuthentication();
            return builder;
        }
    }
}
﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Outloud.Common.Swagger
{
    public static class Extensions
    {
        public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
        {
            SwaggerOptions options;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                services.Configure<SwaggerOptions>(configuration.GetSection("swagger"));
                options = configuration.GetOptions<SwaggerOptions>("swagger");
            }

            if (!options.Enabled)
            {
                return services;
            }

            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = options.Project, Version = options.Version });
            });
        }

        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder builder)
        {
            var options = builder.ApplicationServices.GetService<IConfiguration>()
                .GetOptions<SwaggerOptions>("swagger");
            if (!options.Enabled)
            {
                return builder;
            }

            builder.UseSwagger();
            builder.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.RoutePrefix = string.Empty;
            });
            return builder;
        }
    }
}

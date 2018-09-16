using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Outloud.Common.Logging
{
    public static class Extensions
    {
        public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder, string applicationName = null)
        {
            webHostBuilder.ConfigureServices(s =>
            {
                IConfiguration configuration;
                using (var serviceProvider = s.BuildServiceProvider())
                {
                    configuration = serviceProvider.GetService<IConfiguration>();
                }

                var options = configuration.GetOptions<SentryOptions>("sentry");
                s.AddSingleton(options);
            })
           .ConfigureAppConfiguration((ctx, cfg) =>
           {
               var options = cfg.Build().GetOptions<SentryOptions>("sentry");
               webHostBuilder.UseSentry(options.Dsn);
           });
           return webHostBuilder;
        }
    }
}

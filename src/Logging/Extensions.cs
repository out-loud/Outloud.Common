using Microsoft.AspNetCore.Hosting;

namespace Outloud.Common.Logging
{
    public static class Extensions
    {
        public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder, string applicationName = null) => webHostBuilder.UseSentry("https://e57ed715e8424240a8104773ede02763@sentry.io/1279047");
    }
}

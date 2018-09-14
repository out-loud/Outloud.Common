using Microsoft.AspNetCore.Hosting;
using NSubstitute;
using Outloud.Common.Logging;
using Xunit;

namespace Outloud.Common.Tests.Logging
{
    public class ExtensionsTests
    {
        [Fact]
        public void when_use_logging_called()
        {
            var builder = Substitute.For<IWebHostBuilder>()
                .UseLogging()
                .Build();
        }
    }
}

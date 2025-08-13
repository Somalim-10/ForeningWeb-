using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed;
using Xunit;

namespace ForeningWeb.Tests
{
    public class SessionTests
    {
        [Fact]
        public void Session_Registers_DistributedCache()
        {
            var services = new ServiceCollection();
            services.AddDistributedMemoryCache();
            services.AddSession();

            var provider = services.BuildServiceProvider();
            var cache = provider.GetService<IDistributedCache>();

            Assert.NotNull(cache);
        }
    }
}

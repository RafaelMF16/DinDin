using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DinDin.Tests
{
    public class BaseTest : IDisposable
    {
        protected ServiceProvider ServiceProvider;

        public BaseTest()
        {
            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables().Build();
            var services = new ServiceCollection();
            InjectionModule.AddServicesInScope(services, configuration);
            ServiceProvider = services.BuildServiceProvider();
        }
        public void Dispose()
        {
            ServiceProvider.Dispose();
        }
    }
}
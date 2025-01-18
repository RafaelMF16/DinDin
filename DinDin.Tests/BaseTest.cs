using Microsoft.Extensions.DependencyInjection;

namespace DinDin.Tests
{
    public class BaseTest : IDisposable
    {
        protected ServiceProvider ServiceProvider;

        public BaseTest()
        {
            var services = new ServiceCollection();
            InjectionModule.AddServicesInScope(services);
            ServiceProvider = services.BuildServiceProvider();
        }
        public void Dispose()
        {
            ServiceProvider.Dispose();
        }
    }
}
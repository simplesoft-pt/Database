using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleSoft.Database.EFCore
{
    public class ServiceProviderFixture
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ServiceProviderFixture()
        {
            _scopeFactory = TestSingletons.Provider.GetRequiredService<IServiceScopeFactory>();
        }

        public async Task ExecuteAsync<TService>(Func<TService, Task> executor)
        {
            using var scope = _scopeFactory.CreateScope();

            await executor(
                scope.ServiceProvider.GetRequiredService<TService>()
            );
        }
    }
}
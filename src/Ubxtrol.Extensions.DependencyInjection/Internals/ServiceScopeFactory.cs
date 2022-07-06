using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ubxtrol.Extensions.DependencyInjection
{
    internal class ServiceScopeFactory : IServiceScopeFactory
    {
        private readonly ServiceConfiguration configuration;

        private readonly ServiceContainer container;

        public ServiceScopeFactory(ServiceConfiguration configuration, ServiceContainer container)
        {
            if (configuration == null)
                throw Error.ArgumentNull(nameof(configuration));

            if (container == null)
                throw Error.ArgumentNull(nameof(container));

            this.configuration = configuration;
            this.container = container;
        }

        public IServiceScope CreateScope()
        {
            if (this.container.IsDisposed)
                throw Error.Disposed(nameof(IServiceProvider));

            ScopedServiceProvider provider = new ScopedServiceProvider(this.configuration, this.container);
            return new ServiceScope(provider);
        }
    }
}

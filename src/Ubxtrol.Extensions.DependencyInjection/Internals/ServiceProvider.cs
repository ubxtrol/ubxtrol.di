using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Ubxtrol.Extensions.DependencyInjection
{
    internal class ServiceProvider : IAsyncDisposable, IDisposable, IServiceProvider, ISupportRequiredService
    {
        private readonly ServiceConfiguration configuration;

        private readonly ServiceContainer container;

        private readonly IServiceScopeValidator validator;

        public ServiceConfiguration Configuration => this.configuration;

        public ServiceContainer Container => this.container;

        private object ResolveDependencyResolution(Type mServiceType, IDependencyResolution resolution)
        {
            if (resolution == null)
                throw Error.ArgumentNull(nameof(resolution));

            this.validator.BeforeDependencyResolutionResolved(mServiceType);
            DependencyResolveContext context = this.CreateResolveContext();
            resolution.Resolve(context);
            return context.Result;
        }

        protected ServiceProvider(ServiceConfiguration configuration, IServiceScopeValidator validator)
        {
            if (configuration == null)
                throw Error.ArgumentNull(nameof(configuration));

            if (validator == null)
                throw Error.ArgumentNull(nameof(validator));

            this.configuration = configuration;
            this.container = new ServiceContainer();
            this.validator = validator;
        }

        protected virtual DependencyResolveContext CreateResolveContext()
        {
            return new DependencyResolveContext(this);
        }

        public ServiceProvider(ServiceConfiguration configuration)
            : this(configuration, configuration?.Validator)
        { }

        public void Dispose()
        {
            this.container.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            return this.container.DisposeAsync();
        }

        public object GetRequiredService(Type mServiceType)
        {
            if (this.container.IsDisposed)
                throw Error.Disposed(nameof(IServiceProvider));

            IDependencyResolution resolution = this.configuration.GetResolution(mServiceType);
            if (EmptyDependencyResolution.Shared.Equals(resolution))
                throw Error.Invalid($"服务[{mServiceType}]尚未注册!");

            return this.ResolveDependencyResolution(mServiceType, resolution);
        }

        public object GetService(Type mServiceType)
        {
            if (this.container.IsDisposed)
                throw Error.Disposed(nameof(IServiceProvider));

            IDependencyResolution resolution = this.configuration.GetResolution(mServiceType);
            return this.ResolveDependencyResolution(mServiceType, resolution);
        }
    }
}

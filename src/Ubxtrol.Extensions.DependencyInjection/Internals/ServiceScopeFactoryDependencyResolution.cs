using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ubxtrol.Extensions.DependencyInjection
{
    internal class ServiceScopeFactoryDependencyResolution : ServiceDependencyResolution
    {
        private static readonly Lazy<IDependencyResolution> Cache = new Lazy<IDependencyResolution>(() => new ServiceScopeFactoryDependencyResolution());

        public static IDependencyResolution Shared => ServiceScopeFactoryDependencyResolution.Cache.Value;

        private ServiceScopeFactoryDependencyResolution()
            : base(ServiceIdentity.From(0x0, typeof(IServiceScopeFactory), ServiceLifetime.Singleton))
        { }

        protected override void ExecuteServiceCreation(DependencyResolveContext context)
        {
            if (context == null)
                throw Error.ArgumentNull(nameof(context));

            ServiceContainer container = context.Container;
            if (container == null)
                container = context.Provider.Container;

            ServiceConfiguration configuration = context.Provider.Configuration;
            context.Result = new ServiceScopeFactory(configuration, container);
        }
    }
}

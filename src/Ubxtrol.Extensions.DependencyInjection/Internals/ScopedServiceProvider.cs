namespace Ubxtrol.Extensions.DependencyInjection
{
    internal class ScopedServiceProvider : ServiceProvider
    {
        private readonly ServiceContainer container;

        protected override DependencyResolveContext CreateResolveContext()
        {
            DependencyResolveContext result = new DependencyResolveContext(this);
            result.Container = this.container;
            return result;
        }

        public ScopedServiceProvider(ServiceConfiguration configuration, ServiceContainer container)
            : base(configuration, EmptyServiceScopeValidator.Shared)
        {
            if (container == null)
                throw Error.ArgumentNull(nameof(container));

            this.container = container;
        }
    }
}

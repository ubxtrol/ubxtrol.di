namespace Ubxtrol.Extensions.DependencyInjection
{
    internal class DependencyResolveContext
    {
        private readonly ServiceProvider provider;

        public ServiceContainer Container { get; set; }

        public ServiceProvider Provider => this.provider;

        public object Result { get; set; }

        public DependencyResolveContext(ServiceProvider provider)
        {
            if (provider == null)
                throw Error.ArgumentNull(nameof(provider));

            this.provider = provider;
        }
    }
}

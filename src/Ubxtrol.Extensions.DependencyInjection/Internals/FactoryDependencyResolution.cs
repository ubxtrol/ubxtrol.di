using System;

namespace Ubxtrol.Extensions.DependencyInjection
{
    internal class FactoryDependencyResolution : ServiceDependencyResolution
    {
        private readonly Func<IServiceProvider, object> method;

        protected override void ExecuteServiceCreation(DependencyResolveContext context)
        {
            if (context == null)
                throw Error.ArgumentNull(nameof(context));

            context.Result = this.method.Invoke(context.Provider);
        }

        public FactoryDependencyResolution(ServiceIdentity identity, Func<IServiceProvider, object> method)
            : base(identity)
        {
            if (method == null)
                throw Error.ArgumentNull(nameof(method));

            this.method = method;
        }
    }
}

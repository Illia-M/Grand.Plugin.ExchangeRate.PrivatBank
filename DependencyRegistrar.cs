using Autofac;
using Grand.Core.Configuration;
using Grand.Core.Infrastructure;
using Grand.Core.Infrastructure.DependencyManagement;

namespace Grand.Plugin.ExchangeRate.PrivatBank
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, GrandConfig config)
        {
            builder.RegisterType<PrivatBankExchangeRateProvider>().InstancePerLifetimeScope();
            builder.RegisterType<PrivatBankClient>();
        }

        public int Order => 1;
    }
}

using Grand.Core.Configuration;
using Grand.Core.DependencyInjection;
using Grand.Core.TypeFinders;
using Microsoft.Extensions.DependencyInjection;

namespace Grand.Plugin.ExchangeRate.PrivatBank
{
    public class DependencyRegistrar : IDependencyInjection
    {
        public void Register(IServiceCollection serviceCollection, ITypeFinder typeFinder, GrandConfig config)
        {
            serviceCollection.AddTransient<PrivatBankExchangeRateProvider>();
            serviceCollection.AddTransient<PrivatBankClient>();
        }

        public int Order => 1;
    }
}

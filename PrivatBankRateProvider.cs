using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Grand.Core;
using Grand.Core.Plugins;
using Grand.Services.Directory;
using Grand.Services.Localization;

[assembly: InternalsVisibleTo("Grand.Plugin.Tests")]

namespace Grand.Plugin.ExchangeRate.PrivatBank
{
    public class PrivatBankRateProvider : BasePlugin, IExchangeRateProvider
    {
        private readonly ILocalizationService _localizationService;
        private readonly ILanguageService _languageService;

        public PrivatBankRateProvider(ILocalizationService localizationService, ILanguageService languageService)
        {
            _localizationService = localizationService;
            _languageService = languageService;
        }

        /// <summary>
        /// Gets currency live rates
        /// </summary>
        /// <param name="exchangeRateCurrencyCode">Exchange rate currency code</param>
        /// <returns>Exchange rates</returns>
        public async Task<IList<Core.Domain.Directory.ExchangeRate>> GetCurrencyLiveRates(string exchangeRateCurrencyCode)
        {
            if (string.IsNullOrEmpty(exchangeRateCurrencyCode))
            {
                throw new ArgumentNullException(nameof(exchangeRateCurrencyCode));
            }

            var client = new PrivatBankClient();
            var exchangeRates = await client.GetRates();
            var atTime = DateTimeOffset.UtcNow;

            return exchangeRates
                .Where(dto => dto.BaseCurrency.Equals(exchangeRateCurrencyCode, StringComparison.OrdinalIgnoreCase))
                .Where(dto => dto.Sale != default)
                .Select(dto => new Core.Domain.Directory.ExchangeRate {
                    CurrencyCode = dto.Currency.ToUpper(),
                    Rate = 1 / dto.Sale,
                    UpdatedOn = atTime.DateTime,
                })
                .ToList();
        }

        public override async Task Install()
        {
            //locales
            ////await this.AddOrUpdatePluginLocaleResource(_localizationService, _languageService, "Plugins.ExchangeRate.PrivatBankExchange.SetCurrencyToEURO", "You can use PrivatBank exchange rate provider only when the primary exchange rate currency is set to ???");
            await base.Install();
        }

        public override async Task Uninstall()
        {
            //locales
            ////await this.DeletePluginLocaleResource(_localizationService, _languageService, "Plugins.ExchangeRate.PrivatBankExchange.SetCurrencyToEURO");
            await base.Uninstall();
        }
    }
}

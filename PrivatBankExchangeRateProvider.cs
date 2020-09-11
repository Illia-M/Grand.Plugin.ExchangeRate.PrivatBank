using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grand.Core.Plugins;
using Grand.Services.Directory;

namespace Grand.Plugin.ExchangeRate.PrivatBank
{
    internal sealed class PrivatBankExchangeRateProvider : BasePlugin, IExchangeRateProvider
    {
        private readonly PrivatBankClient _privatBankClient;

        public PrivatBankExchangeRateProvider(PrivatBankClient privatBankClient)
        {
            _privatBankClient = privatBankClient;
        } 

        public async Task<IList<Domain.Directory.ExchangeRate>> GetCurrencyLiveRates(string exchangeRateCurrencyCode)
        {
            if (string.IsNullOrEmpty(exchangeRateCurrencyCode))
            {
                throw new ArgumentNullException(nameof(exchangeRateCurrencyCode));
            }

            var exchangeRates = await _privatBankClient.GetRates();
            var atTime = DateTimeOffset.UtcNow;

            return exchangeRates
                .Where(dto => dto.BaseCurrency.Equals(exchangeRateCurrencyCode, StringComparison.OrdinalIgnoreCase))
                .Where(dto => dto.Sale != default)
                .Select(dto => new Domain.Directory.ExchangeRate {
                    CurrencyCode = dto.Currency.ToUpper(),
                    Rate = 1 / dto.Sale,
                    UpdatedOn = atTime.DateTime,
                })
                .ToList();
        }
    }
}

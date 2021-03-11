using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Grand.Plugin.ExchangeRate.PrivatBank
{
    internal class PrivatBankClient
    {
        private readonly ILogger _logger;

        public PrivatBankClient(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<ExchangeRateDto[]> GetRates()
        {
            try
            {
                using var webClient = new HttpClient { BaseAddress = new Uri("https://api.privatbank.ua/") };

                var json = await webClient.GetStringAsync("p24api/pubinfo?json&exchange&coursid=5");
                var response = JsonSerializer.Deserialize<ExchangeRateDto[]>(json);

                return response ?? throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on get exchange rate from Private");
                return Array.Empty<ExchangeRateDto>();
            }
        }
    }
}
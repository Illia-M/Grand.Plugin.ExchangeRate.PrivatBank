using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Grand.Plugin.ExchangeRate.PrivatBank
{
    internal class PrivatBankClient
    {
        public async Task<ExchangeRateDto[]> GetRates()
        {
            try
            {
                var URLString = "https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5";
                using var webClient = new System.Net.WebClient();
                var json = await webClient.DownloadStringTaskAsync(URLString);
                var response = JsonConvert.DeserializeObject<ExchangeRateDto[]>(json);

                return response ?? throw new InvalidOperationException();
            }
            catch (Exception)
            {
                return Array.Empty<ExchangeRateDto>();
            }
        }
    }
}
using System.Text.Json.Serialization;

namespace Grand.Plugin.ExchangeRate.PrivatBank
{
    internal class ExchangeRateDto
    {
        [JsonPropertyName("ccy")]
        public string Currency { get; set; }

        [JsonPropertyName("base_ccy")]
        public string BaseCurrency { get; set; }

        [JsonPropertyName("buy")]
        public decimal Buy { get; set; }

        [JsonPropertyName("sale")]
        public decimal Sale { get; set; }
    }
}
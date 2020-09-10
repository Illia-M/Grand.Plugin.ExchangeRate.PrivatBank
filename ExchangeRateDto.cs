using System.Runtime.Serialization;

namespace Grand.Plugin.ExchangeRate.PrivatBank
{
    [DataContract]
    internal class ExchangeRateDto
    {
        [DataMember(Name = "ccy")]
        public string Currency { get; set; }

        [DataMember(Name = "base_ccy")]
        public string BaseCurrency { get; set; }

        [DataMember(Name = "buy")]
        public decimal Buy { get; set; }

        [DataMember(Name = "sale")]
        public decimal Sale { get; set; }
    }
}
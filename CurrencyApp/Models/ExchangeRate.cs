using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyApp.Models
{
    public partial class ExchangeRate
    {
        [JsonProperty("baseCurrency")]
        public string BaseCurrency { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("saleRateNB")]
        public double SaleRateNb { get; set; }

        [JsonProperty("purchaseRateNB")]
        public double PurchaseRateNb { get; set; }

        [JsonProperty("saleRate", NullValueHandling = NullValueHandling.Ignore)]
        public double? SaleRate { get; set; }

        [JsonProperty("purchaseRate", NullValueHandling = NullValueHandling.Ignore)]
        public double? PurchaseRate { get; set; }
    }
}

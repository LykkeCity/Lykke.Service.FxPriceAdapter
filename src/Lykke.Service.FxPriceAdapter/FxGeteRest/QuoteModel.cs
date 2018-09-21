using System;
using Newtonsoft.Json;

namespace Lykke.Service.FxPriceAdapter.FxGeteRest
{
    public class QuoteModel
    {
        [JsonProperty("s")]
        public string Symbol { get; set; }

        [JsonProperty("r")]
        public decimal MidPrice { get; set; }

        [JsonProperty("sp")]
        public decimal Spread { get; set; }

        [JsonProperty("dt")]
        public DateTime DateTime { get; set; }

        [JsonProperty("n")]
        public long Number { get; set; }

        [JsonProperty("iw")]
        public bool IsWorkTime { get; set; }

        [JsonProperty("om")]
        public bool IsOpenMarket { get; set; }
    }
}

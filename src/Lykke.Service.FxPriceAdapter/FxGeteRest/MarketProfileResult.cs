using System;

namespace Lykke.Service.FxPriceAdapter.FxGeteRest
{
    public class MarketProfileResult
    {
        public DateTime Time { get; set; }
        public long Version { get; set; }
        public QuoteModel[] QuotesTrade { get; set; }
    }
}
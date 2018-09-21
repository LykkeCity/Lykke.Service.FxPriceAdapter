namespace Lykke.Service.FxPriceAdapter.RabbitPublishers
{
    public class FxTickPrice : Lykke.Common.ExchangeAdapter.Contracts.TickPrice
    {
        public bool IsWorkTime { get; set; }
        public bool IsOpenMarket { get; set; }
        public long Number { get; set; }
    }
}

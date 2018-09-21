using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Common;
using Lykke.Common.Log;
using Lykke.Service.FxPriceAdapter.FxGeteRest;
using Lykke.Service.FxPriceAdapter.RabbitPublishers;

namespace Lykke.Service.FxPriceAdapter.PeriodicalHandlers
{
    public class MyPeriodicalHandler : IStartable, IStopable
    {
        private readonly IFxGeteRestClient _fxGeteRestClient;
        private readonly ITickPricePublisher _pricePublisher;
        private readonly TimerTrigger _timerTrigger;
        private readonly Dictionary<string, FxTickPrice> _lastPrices = new Dictionary<string, FxTickPrice>();
        private DateTime _nextForcePush;

        public const string ExchangeName = "fxprice";

        public MyPeriodicalHandler(ILogFactory logFactory, IFxGeteRestClient fxGeteRestClient, ITickPricePublisher pricePublisher, TimeSpan interval)
        {
            _fxGeteRestClient = fxGeteRestClient;
            _pricePublisher = pricePublisher;
            _timerTrigger = new TimerTrigger(nameof(MyPeriodicalHandler), interval, logFactory);
            _timerTrigger.Triggered += Execute;

            _nextForcePush = DateTime.UtcNow;
        }

        public void Start()
        {
            _timerTrigger.Start();
        }
        
        public void Stop()
        {
            _timerTrigger.Stop();
        }

        public void Dispose()
        {
            _timerTrigger.Stop();
            _timerTrigger.Dispose();
        }

        private async Task Execute(ITimerTrigger timer, TimerTriggeredHandlerArgs args, CancellationToken cancellationToken)
        {
            var data = await _fxGeteRestClient.GetMarketProfileAsync();

            var ticks = data.QuotesTrade.Select(ConvertToTick);

            var isForcePush = _nextForcePush <= DateTime.UtcNow;

            foreach (var tick in ticks)
            {
                if (!_lastPrices.ContainsKey(tick.Asset) || _lastPrices[tick.Asset].Timestamp < tick.Timestamp || isForcePush)
                {
                    await _pricePublisher.PublishAsync(tick);
                    _lastPrices[tick.Asset] = tick;
                }
            }

            if (isForcePush)
                _nextForcePush = DateTime.UtcNow.AddMinutes(1);
        }

        private FxTickPrice ConvertToTick(QuoteModel quote)
        {
            var tick = new FxTickPrice
            {
                Source = ExchangeName,
                Asset = quote.Symbol,
                IsOpenMarket = quote.IsOpenMarket,
                IsWorkTime = quote.IsWorkTime,
                Number = quote.Number,
                Timestamp = quote.DateTime,
                Ask = quote.MidPrice + quote.Spread / 2m,
                Bid = quote.MidPrice - quote.Spread / 2m
            };
            return tick;
        }
    }
}

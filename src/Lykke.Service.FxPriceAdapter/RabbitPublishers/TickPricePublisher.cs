using System.Threading.Tasks;
using Autofac;
using Common;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Publisher;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.FxPriceAdapter.Settings;

namespace Lykke.Service.FxPriceAdapter.RabbitPublishers
{
    public class TickPricePublisher : ITickPricePublisher,IStartable, IStopable
    {
        private readonly ILogFactory _logFactory;
        private readonly RabbitMqSettings _settting;
        private readonly string _connectionString;
        private RabbitMqPublisher<FxTickPrice> _publisher;

        public TickPricePublisher(ILogFactory logFactory, RabbitMqSettings settting)
        {
            _logFactory = logFactory;
            _settting = settting;
        }

        public void Start()
        {
            // NOTE: Read https://github.com/LykkeCity/Lykke.RabbitMqDotNetBroker/blob/master/README.md to learn
            // about RabbitMq subscriber configuration

            var settings = RabbitMqSubscriptionSettings
                .ForPublisher(_settting.ConnectionString, _settting.ExchangeName);

            _publisher = new RabbitMqPublisher<FxTickPrice>(_logFactory, settings)
                .SetSerializer(new JsonMessageSerializer<FxTickPrice>())
                .SetPublishStrategy(new DefaultFanoutPublishStrategy(settings))
                .PublishSynchronously()
                .Start();
        }

        public void Dispose()
        {
            _publisher?.Dispose();
        }

        public void Stop()
        {
            _publisher?.Stop();
        }

        public async Task PublishAsync(FxTickPrice message)
        {
            await _publisher.ProduceAsync(message);
        }
    }
}

using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.FxPriceAdapter.Settings
{
    public class RabbitMqSettings
    {
        [AmqpCheck]
        public string ConnectionString { get; set; }

        public string ExchangeName { get; set; }
    }
}

using Lykke.SettingsReader.Attributes;

namespace Lykke.Job.FxPriceAdapter.Settings.JobSettings
{
    public class RabbitMqSettings
    {
        [AmqpCheck]
        public string ConnectionString { get; set; }
    }
}

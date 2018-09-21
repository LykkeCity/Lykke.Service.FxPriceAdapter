namespace Lykke.Job.FxPriceAdapter.Settings.JobSettings
{
    public class FxPriceAdapterJobSettings
    {
        public DbSettings Db { get; set; }
        public RabbitMqSettings Rabbit { get; set; }
    }
}

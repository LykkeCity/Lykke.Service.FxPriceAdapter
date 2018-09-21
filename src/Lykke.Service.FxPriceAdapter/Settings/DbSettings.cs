using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.FxPriceAdapter.Settings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnectionString { get; set; }
    }
}

using Lykke.Job.FxPriceAdapter.Settings.JobSettings;
using Lykke.Job.FxPriceAdapter.Settings.SlackNotifications;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Job.FxPriceAdapter.Settings
{
    public class AppSettings
    {
        public FxPriceAdapterJobSettings FxPriceAdapterJob { get; set; }

        public SlackNotificationsSettings SlackNotifications { get; set; }

        [Optional]
        public MonitoringServiceClientSettings MonitoringServiceClient { get; set; }
    }
}

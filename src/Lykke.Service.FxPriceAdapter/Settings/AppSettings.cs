using JetBrains.Annotations;
using Lykke.Sdk.Settings;

namespace Lykke.Service.FxPriceAdapter.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public FxPriceAdapterSettings FxPriceAdapterService { get; set; }
    }
}

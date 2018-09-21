using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.FxPriceAdapter.Client 
{
    /// <summary>
    /// FxPriceAdapter client settings.
    /// </summary>
    public class FxPriceAdapterServiceClientSettings 
    {
        /// <summary>Service url.</summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl {get; set;}
    }
}

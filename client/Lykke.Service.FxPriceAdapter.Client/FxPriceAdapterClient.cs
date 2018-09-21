using Lykke.HttpClientGenerator;

namespace Lykke.Service.FxPriceAdapter.Client
{
    /// <summary>
    /// FxPriceAdapter API aggregating interface.
    /// </summary>
    public class FxPriceAdapterClient : IFxPriceAdapterClient
    {
        // Note: Add similar Api properties for each new service controller

        /// <summary>Inerface to FxPriceAdapter Api.</summary>
        public IFxPriceAdapterApi Api { get; private set; }

        /// <summary>C-tor</summary>
        public FxPriceAdapterClient(IHttpClientGenerator httpClientGenerator)
        {
            Api = httpClientGenerator.Generate<IFxPriceAdapterApi>();
        }
    }
}

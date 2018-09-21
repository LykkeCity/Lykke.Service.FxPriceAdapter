using JetBrains.Annotations;

namespace Lykke.Service.FxPriceAdapter.Client
{
    /// <summary>
    /// FxPriceAdapter client interface.
    /// </summary>
    [PublicAPI]
    public interface IFxPriceAdapterClient
    {
        // Make your app's controller interfaces visible by adding corresponding properties here.
        // NO actual methods should be placed here (these go to controller interfaces, for example - IFxPriceAdapterApi).
        // ONLY properties for accessing controller interfaces are allowed.

        /// <summary>Application Api interface</summary>
        IFxPriceAdapterApi Api { get; }
    }
}

using System.Threading.Tasks;

namespace Lykke.Service.FxPriceAdapter.RabbitPublishers
{
    public interface ITickPricePublisher
    {
        Task PublishAsync(FxTickPrice message);
    }
}

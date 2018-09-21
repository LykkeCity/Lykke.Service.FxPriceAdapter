using System.Threading.Tasks;
using Autofac;
using Common;
using Lykke.Job.FxPriceAdapter.Contract;

namespace Lykke.Service.FxPriceAdapter.Domain.Services
{
    public interface IMyRabbitPublisher : IStartable, IStopable
    {
        Task PublishAsync(MyPublishedMessage message);
    }
}
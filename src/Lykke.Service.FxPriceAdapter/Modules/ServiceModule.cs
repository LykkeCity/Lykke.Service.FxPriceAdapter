using Autofac;
using Common;
using Lykke.Service.FxPriceAdapter.FxGeteRest;
using Lykke.Service.FxPriceAdapter.PeriodicalHandlers;
using Lykke.Service.FxPriceAdapter.RabbitPublishers;
using Lykke.Service.FxPriceAdapter.Settings;
using Lykke.SettingsReader;

namespace Lykke.Service.FxPriceAdapter.Modules
{
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<AppSettings> _appSettings;
        private FxPriceAdapterSettings _settings;

        public ServiceModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings;
            _settings = _appSettings.CurrentValue.FxPriceAdapterService;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MyPeriodicalHandler>()
                .As<IStartable>()
                .As<IStopable>()
                .SingleInstance()
                .WithParameter(TypedParameter.From(_settings.Rabbit.ConnectionString));

            builder.RegisterType<TickPricePublisher>()
                .As<ITickPricePublisher>()
                .As<IStartable>()
                .SingleInstance()
                .WithParameter(TypedParameter.From(_settings.Rabbit));

            builder.RegisterType<FxGeteRestClient>()
                .As<IFxGeteRestClient>()
                .SingleInstance()
                .WithParameter(TypedParameter.From(_settings.SourceUrl));
            
        }
    }
}

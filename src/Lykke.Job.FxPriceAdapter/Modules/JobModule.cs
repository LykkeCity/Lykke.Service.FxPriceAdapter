using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common;
using Common.Log;
using Lykke.Job.FxPriceAdapter.Services;
using Lykke.Job.FxPriceAdapter.Settings.JobSettings;
using Lykke.Sdk;
using Lykke.Sdk.Health;
using Lykke.Job.FxPriceAdapter.PeriodicalHandlers;
using AzureStorage.Blob;
using Lykke.Job.FxPriceAdapter.Contract;
using Lykke.Job.FxPriceAdapter.RabbitPublishers;
using Lykke.RabbitMq.Azure;
using Lykke.RabbitMqBroker.Publisher;
using Lykke.Service.FxPriceAdapter.Domain.Services;
using Lykke.SettingsReader;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Job.FxPriceAdapter.Modules
{
    public class JobModule : Module
    {
        private readonly FxPriceAdapterJobSettings _settings;
        private readonly IReloadingManager<FxPriceAdapterJobSettings> _settingsManager;
        // NOTE: you can remove it if you don't need to use IServiceCollection extensions to register service specific dependencies
        private readonly IServiceCollection _services;

        public JobModule(FxPriceAdapterJobSettings settings, IReloadingManager<FxPriceAdapterJobSettings> settingsManager)
        {
            _settings = settings;
            _settingsManager = settingsManager;

            _services = new ServiceCollection();
        }

        protected override void Load(ContainerBuilder builder)
        {
            // NOTE: Do not register entire settings in container, pass necessary settings to services which requires them
            // ex:
            // builder.RegisterType<QuotesPublisher>()
            //  .As<IQuotesPublisher>()
            //  .WithParameter(TypedParameter.From(_settings.Rabbit.ConnectionString))

            builder.RegisterType<HealthService>()
                .As<IHealthService>()
                .SingleInstance();

            builder.RegisterType<StartupManager>()
                .As<IStartupManager>()
                .SingleInstance();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>()
                .AutoActivate()
                .SingleInstance();

            RegisterPeriodicalHandlers(builder);

            RegisterRabbitMqPublishers(builder);

            // TODO: Add your dependencies here

            builder.Populate(_services);
        }

        private void RegisterPeriodicalHandlers(ContainerBuilder builder)
        {
            // TODO: You should register each periodical handler in DI container as IStartable singleton and autoactivate it

            builder.RegisterType<MyPeriodicalHandler>()
                .As<IStartable>()
                .As<IStopable>()
                .SingleInstance();
        }

        private void RegisterRabbitMqPublishers(ContainerBuilder builder)
        {
            // TODO: You should register each publisher in DI container as publisher specific interface and as IStartable,
            // as singleton and do not autoactivate it

            builder.RegisterType<MyRabbitPublisher>()
                .As<IMyRabbitPublisher>()
                .As<IStartable>()
                .SingleInstance()
                .WithParameter(TypedParameter.From(_settings.Rabbit.ConnectionString));
        }
    }
}

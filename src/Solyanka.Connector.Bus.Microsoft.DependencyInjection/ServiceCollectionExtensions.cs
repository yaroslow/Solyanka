using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Solyanka.Connector.Bus.Microsoft.DependencyInjection
{
    /// <summary>
    /// Class-extensions for adding MassTransit to DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add MassTransit bus and consumers
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="consumersConfiguration"><see cref="ConsumersConfiguration"/></param>
        /// <param name="busSettings"><see cref="IBusControl"/></param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddMassTransitInfrastructure(this IServiceCollection services,
            Action<ConsumersConfiguration> consumersConfiguration,
            Func<IServiceProvider, IBusControl> busSettings)
        {
            var consumersConfig = new ConsumersConfiguration();
            consumersConfiguration.Invoke(consumersConfig);
            
            services.AddMassTransit(options =>
            {
                options.AddConsumers(consumersConfig.ConsumerTypes.ToArray());
                options.AddBus(busSettings);
            });

            return services;
        }
    }
}
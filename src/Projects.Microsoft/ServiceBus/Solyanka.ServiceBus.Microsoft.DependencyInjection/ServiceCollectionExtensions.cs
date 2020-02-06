using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Solyanka.ServiceBus;
using Solyanka.ServiceBus.Abstractions;
using Solyanka.ServiceBus.Microsoft.DependencyInjection.Infrastructure;

namespace Solyanka.ServiceBus.Microsoft.DependencyInjection
{
    /// <summary>
    /// Class-extension over <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add service bus core dependencies to Microsoft DI container
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddServiceBusCore(this IServiceCollection services)
        {
            services.AddSingleton<IHostedService, BusHostedService>();

            services.AddScoped<IServiceBus, ServiceBus>();
            
            return services;
        }
        
        /// <summary>
        /// Add service bus
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="busFactory">Configured bus factory</param>
        /// <param name="consumersConfigurationAction"><see cref="ConsumersConfiguration"/></param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddServiceBus(this IServiceCollection services, Func<IServiceProvider, IBusControl> busFactory, Action<ConsumersConfiguration> consumersConfigurationAction = null)
        {
            var consumersConfiguration = new ConsumersConfiguration();
            consumersConfigurationAction?.Invoke(consumersConfiguration);

            services.AddMassTransit(options =>
            {
                options.AddConsumers(consumersConfiguration.ConsumerTypes.ToArray());
                options.AddBus(busFactory);
            });

            services.AddServiceBusCore();

            return services;
        }
    }
}
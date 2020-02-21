using System;
using System.Collections.Generic;
using System.Reflection;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Solyanka.ServiceBus.Abstractions;
using Solyanka.ServiceBus.Microsoft.DependencyInjection;
using Solyanka.ServiceBus.Microsoft.DependencyInjection.Infrastructure;

namespace Solyanka.ServiceBus.RabbitMq.Microsoft.DependencyInjection
{
    /// <summary>
    /// Class-extension over <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add service bus over RabbitMQ
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="assembliesSetup">Assemblies where to find <see cref="IIntegrationEvent"/></param>
        /// <param name="rabbitMqEndpointSettings">Configuring <see cref="RabbitMqEndpointSettings"/></param>
        /// <param name="consumersConfigurationAction">Configuring <see cref="ConsumersConfiguration"/></param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddRabbitMqServiceBus(this IServiceCollection services, Action<IList<Assembly>> assembliesSetup, Action<RabbitMqEndpointSettings> rabbitMqEndpointSettings, Action<ConsumersConfiguration> consumersConfigurationAction = null)
        {    
            var configuration = new RabbitMqEndpointSettings();
            rabbitMqEndpointSettings.Invoke(configuration);

            services.AddServiceBus(assembliesSetup, provider =>
                {
                    return Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        cfg.Host(configuration.Host, configuration.VirtualHost, h =>
                        {
                            h.Username(configuration.Username);
                            h.Password(configuration.Password);
                        });

                        cfg.ReceiveEndpoint(configuration.ServiceEndpointName, e =>
                        {
                            e.Durable = configuration.Durable;
                            e.PrefetchCount = configuration.PrefetchCount;
                            e.UseMessageRetry(x => x.Interval(configuration.RetryCount, configuration.RetryInterval));
                            e.ConfigureConsumers(provider);
                        });
                    });
                },
                consumersConfigurationAction);

            return services;
        }
    }
}
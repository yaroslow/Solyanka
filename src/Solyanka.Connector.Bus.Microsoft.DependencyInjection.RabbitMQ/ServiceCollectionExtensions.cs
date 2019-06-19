using System;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Solyanka.Connector.Bus.Microsoft.DependencyInjection.RabbitMQ
{
    /// <summary>
    /// Class-extensions for adding MassTransit to DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add MassTransit bus and consumers for RabbitMQ
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="consumersConfiguration"><see cref="ConsumersConfiguration"/></param>
        /// <param name="endpointOptions"><see cref="EndpointOptions"/></param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddMassTransitInfrastructureRabbitMq(this IServiceCollection services, 
            Action<ConsumersConfiguration> consumersConfiguration,
            Action<EndpointOptions> endpointOptions)
        {
            var configuration = new EndpointOptions();
            endpointOptions.Invoke(configuration);
            configuration.Validate();
            
            services.AddMassTransitInfrastructure(consumersConfiguration, provider => 
                MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(configuration.Host, configuration.VirtualHost, h =>
                    {
                        h.Username(configuration.Username);
                        h.Password(configuration.Password);
                    });
                    
                    cfg.ReceiveEndpoint(host, configuration.ServiceEndpoint, e =>
                    {
                        e.Durable = true;
                        e.PrefetchCount = 8;
                        e.UseMessageRetry(x => x.Interval(2, 100));
                        e.ConfigureConsumers(provider);
                    });
                }));

            return services;
        }
    }
}
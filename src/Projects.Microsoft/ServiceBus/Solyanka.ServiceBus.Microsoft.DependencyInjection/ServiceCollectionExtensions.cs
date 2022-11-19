using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Solyanka.Cqrs.Handlers;
using Solyanka.ServiceBus.Abstractions;
using Solyanka.ServiceBus.Microsoft.DependencyInjection.Infrastructure;

namespace Solyanka.ServiceBus.Microsoft.DependencyInjection;

/// <summary>
/// Class-extension over <see cref="IServiceCollection"/> to inject
/// <see cref="IServiceBus"/> to Microsoft DI
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add service bus
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="assembliesSetup">Assemblies where to find <see cref="IIntegrationEvent"/></param>
    /// <param name="busFactory">Configured bus factory</param>
    /// <param name="consumersConfigurationAction"><see cref="ConsumersConfiguration"/></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddServiceBus(this IServiceCollection services, Action<IList<Assembly>> assembliesSetup, 
        Func<IBusRegistrationContext, IBusControl> busFactory, Action<ConsumersConfiguration>? consumersConfigurationAction = null)
    {
        var consumersConfiguration = new ConsumersConfiguration();
        consumersConfigurationAction?.Invoke(consumersConfiguration);
            
        services.AddScoped<IServiceBus, ServiceBus>();
        services.InjectIntegrationEventHandler(assembliesSetup);

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddBus(busFactory);
            busConfigurator.AddConsumers(consumersConfiguration.ConsumerTypes.ToArray());
        });

        services.AddScoped<ServiceBusPublisher>(provider =>
            provider.GetRequiredService<IPublishEndpoint>().Publish);
            
        services.AddSingleton<IHostedService, BusHostedService>();
            
        return services;
    }

        
    private static void InjectIntegrationEventHandler(this IServiceCollection services, Action<IList<Assembly>> assembliesSetup)
    {
        var assemblies = new List<Assembly>();
        assembliesSetup.Invoke(assemblies);
            
        var integrationEventType = typeof(IIntegrationEvent);
        var eventTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => integrationEventType.IsAssignableFrom(t));

        var handlerType = typeof(IEventHandler<>);
        foreach (var eventType in eventTypes)
        {
            var concreteHandlerType = handlerType.MakeGenericType(eventType);
            services.AddScoped(concreteHandlerType, typeof(IntegrationEventsHandler));
        }
    }
}
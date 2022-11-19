using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Solyanka.Cqrs;
using Solyanka.Cqrs.CrossCuttingConcerns;
using Solyanka.Cqrs.Events;
using Solyanka.Cqrs.Handlers;
using Solyanka.Utils;

namespace Solyanka.Dispatcher.Microsoft.DependencyInjection;

/// <summary>
/// Class-extensions over <see cref="IServiceCollection"/> to inject
/// <see cref="IQueryHandler{TIn,TOut}"/>,
/// <see cref="ICommandHandler{TIn,TOut}"/>,
/// <see cref="IEventHandler{TEvent}"/>
/// <see cref="IQueryCrossCuttingConcern{TIn,TOut}"/>,
/// <see cref="ICommandCrossCuttingConcern{TIn,TOut}"/>,
/// <see cref="IRequestDispatcher"/>
/// <see cref="IEventStore"/>
/// to Microsoft DI container
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add dispatcher
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="assembliesSetup">Assemblies where to find</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddDispatcher(this IServiceCollection services, Action<IList<Assembly>> assembliesSetup)
    {
        var assemblies = new List<Assembly>();
        assembliesSetup.Invoke(assemblies);
            
        services.AddScoped<ServiceFactory>(a => a.GetRequiredService);

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(IEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
            
        services.AddScoped<Dispatcher>();
        services.AddScoped<IRequestDispatcher, Dispatcher>();

        services.AddScoped<IEventStore, EventStore>();
            
        return services;
    }

    /// <summary>
    /// Configure query pipeline
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="queryCrossCuttingConcernSetup">Query cross-cutting concerns</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection ConfigureQueryPipeline(this IServiceCollection services,
        Action<IList<Type>> queryCrossCuttingConcernSetup)
    {
        var queryCrossCuttingConcerns = new List<Type>();
        queryCrossCuttingConcernSetup.Invoke(queryCrossCuttingConcerns);
            
        foreach (var queryCrossCuttingConcern in queryCrossCuttingConcerns)
        {
            if (!(queryCrossCuttingConcern.IsClass && queryCrossCuttingConcern.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryCrossCuttingConcern<,>))))
                throw new InvalidOperationException(
                    $"It is impossible to correlate type of {queryCrossCuttingConcern} " +
                    $"with type {typeof(IQueryCrossCuttingConcern<,>)}");

            services.AddScoped(typeof(IQueryCrossCuttingConcern<,>), queryCrossCuttingConcern);
        }

        return services;
    }

    /// <summary>
    /// Configure command pipeline
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="commandCrossCuttingConcernSetup">Command cross-cutting concerns</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection ConfigureCommandPipeline(this IServiceCollection services,
        Action<IList<Type>> commandCrossCuttingConcernSetup)
    {
        var commandCrossCuttingConcerns = new List<Type>();
        commandCrossCuttingConcernSetup.Invoke(commandCrossCuttingConcerns);

        foreach (var commandCrossCuttingConcern in commandCrossCuttingConcerns)
        {
            if (!(commandCrossCuttingConcern.IsClass && commandCrossCuttingConcern.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandCrossCuttingConcern<,>))))
                throw new InvalidOperationException(
                    $"It is impossible to correlate type of {commandCrossCuttingConcern} " +
                    $"with type {typeof(ICommandCrossCuttingConcern<,>)}");

            services.AddScoped(typeof(ICommandCrossCuttingConcern<,>), commandCrossCuttingConcern);
        }

        return services;
    }
}
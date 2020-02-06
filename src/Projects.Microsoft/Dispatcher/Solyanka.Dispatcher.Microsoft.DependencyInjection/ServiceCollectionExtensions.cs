using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Solyanka.Cqs.Abstractions;
using Solyanka.Cqs.Abstractions.Handlers;
using Solyanka.Cqs.Abstractions.PipelineUnits;
using Solyanka.Events.Abstractions;
using Solyanka.Utils;

namespace Solyanka.Dispatcher.Microsoft.DependencyInjection
{
    /// <summary>
    /// Class-extensions of service collection to inject
    /// <see cref="IQueryHandler{TIn,TOut}"/>,
    /// <see cref="ICommandHandler{TIn,TOut}"/>,
    /// <see cref="IQueryPipelineUnit{TIn,TOut}"/>,
    /// <see cref="ICommandPipelineUnit{TIn,TOut}"/>,
    /// <see cref="IEventHandler{TEvent}"/>
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
            
            services.AddScoped<ServiceFactory>(a => a.GetService);

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
            services.AddScoped<IEventDispatcher, Dispatcher>();

            return services;
        }

        /// <summary>
        /// Configure query pipeline
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="queryPipelineUnitsSetup">Query pipeline units</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection ConfigureQueryPipeline(this IServiceCollection services,
            Action<IList<Type>> queryPipelineUnitsSetup)
        {
            var queryPipelineUnits = new List<Type>();
            queryPipelineUnitsSetup.Invoke(queryPipelineUnits);
            
            foreach (var queryPipelineUnit in queryPipelineUnits)
            {
                if (!(queryPipelineUnit.IsClass && queryPipelineUnit.GetInterfaces().Any(i =>
                          i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryPipelineUnit<,>))))
                    throw new InvalidOperationException(
                        $"It is impossible to correlate type of {queryPipelineUnit} with type {typeof(IQueryPipelineUnit<,>)}");

                services.AddScoped(typeof(IQueryPipelineUnit<,>), queryPipelineUnit);
            }

            return services;
        }

        /// <summary>
        /// Configure command pipeline
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="commandPipelineUnitsSetup">Command pipeline units</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection ConfigureCommandPipeline(this IServiceCollection services,
            Action<IList<Type>> commandPipelineUnitsSetup)
        {
            var commandPipelineUnits = new List<Type>();
            commandPipelineUnitsSetup.Invoke(commandPipelineUnits);

            foreach (var commandPipelineUnit in commandPipelineUnits)
            {
                if (!(commandPipelineUnit.IsClass && commandPipelineUnit.GetInterfaces().Any(i =>
                          i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandPipelineUnit<,>))))
                    throw new InvalidOperationException(
                        $"It is impossible to correlate type of {commandPipelineUnit} with type {typeof(ICommandPipelineUnit<,>)}");

                services.AddScoped(typeof(ICommandPipelineUnit<,>), commandPipelineUnit);
            }

            return services;
        }
    }
}
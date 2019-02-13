using System;
using System.Collections.Generic;

namespace Architecture.Utils
{
    /// <summary>
    /// Delegate describing the way of getting service
    /// </summary>
    /// <param name="serviceType">Type of desired service</param>
    /// <returns>Desired service</returns>
    public delegate object ServiceFactory(Type serviceType);

    /// <summary>
    /// Extension methods over <see cref="ServiceFactory"/>
    /// </summary>
    public static class ServiceFactoryExtensions
    {
        /// <summary>
        /// Method to get desired service
        /// </summary>
        /// <typeparam name="TService">Type of desired service</typeparam>
        /// <param name="serviceFactory"><see cref="ServiceFactory"/></param>
        /// <returns>Desired service</returns>
        public static TService GetService<TService>(this ServiceFactory serviceFactory) =>
            (TService)serviceFactory(typeof(TService));

        /// <summary>
        /// Method to get all services of desired type
        /// </summary>
        /// <typeparam name="TService">Type of desired service</typeparam>
        /// <param name="serviceFactory"><see cref="ServiceFactory"/></param>
        /// <returns><see cref="IEnumerable{TService}"/></returns>
        public static IEnumerable<TService> GetServices<TService>(this ServiceFactory serviceFactory) =>
            (IEnumerable<TService>)serviceFactory(typeof(IEnumerable<TService>));
    }
}

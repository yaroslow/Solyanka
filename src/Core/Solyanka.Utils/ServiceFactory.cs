namespace Solyanka.Utils;

/// <summary>
/// Delegate defining the procedure for obtaining the service
/// </summary>
/// <param name="serviceType">Service type</param>
/// <returns>Service packed to <see cref="object"/></returns>
public delegate object ServiceFactory(Type serviceType);

    
/// <summary>
/// Class-extension over <see cref="ServiceFactory"/>
/// </summary>
public static class ServiceFactoryExtensions
{
    /// <summary>
    /// Getting service
    /// </summary>
    /// <typeparam name="TService">Service type</typeparam>
    /// <param name="serviceFactory"><see cref="ServiceFactory"/></param>
    /// <returns>Specified service</returns>
    public static TService GetService<TService>(this ServiceFactory serviceFactory) =>
        (TService)serviceFactory(typeof(TService));

    /// <summary>
    /// Getting collection of services
    /// </summary>
    /// <typeparam name="TService">Service type</typeparam>
    /// <param name="serviceFactory"><see cref="ServiceFactory"/></param>
    /// <returns><see cref="IEnumerable{T}"/> of specified services</returns>
    public static IEnumerable<TService> GetServices<TService>(this ServiceFactory serviceFactory) =>
        (IEnumerable<TService>)serviceFactory(typeof(IEnumerable<TService>));
}
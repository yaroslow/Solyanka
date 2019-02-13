using System;
using System.Collections.Generic;

namespace Architecture.Utils
{
    public delegate object ServiceFactory(Type serviceType);

    public static class ServiceFactoryExtensions
    {
        public static TService GetService<TService>(this ServiceFactory serviceFactory) =>
            (TService)serviceFactory(typeof(TService));

        public static IEnumerable<TService> GetServices<TService>(this ServiceFactory serviceFactory) =>
            (IEnumerable<TService>)serviceFactory(typeof(IEnumerable<TService>));
    }
}

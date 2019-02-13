using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DE.Abstractions;
using Architecture.Utils;

namespace Architecture.Internal
{
    internal class Notificator<TNotification> : INotificator where TNotification : INotification
    {
        public Task Notify(INotification notification, CancellationToken cancellationToken, ServiceFactory serviceFactory, Func<IEnumerable<Func<Task>>, Task> notifier)
        {
            var handlers = serviceFactory
                .GetServices<INotificationHandler<TNotification>>()
                .Select(a => new Func<Task>(() => a.Handle((TNotification) notification, cancellationToken)));

            return notifier(handlers);
        }
    }
}

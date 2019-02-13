using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Architecture.Utils;

namespace Architecture.DE.Abstractions
{
    public interface INotificator
    {
        Task Notify(INotification notification, CancellationToken cancellationToken, ServiceFactory serviceFactory, Func<IEnumerable<Func<Task>>, Task> notifier);
    }
}

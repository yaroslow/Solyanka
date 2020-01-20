using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solyanka.Events.Abstractions;
using Solyanka.Utils;

namespace Solyanka.Dispatcher.Internal.Events
{
    internal class Notifier<TEvent> : INotifier where TEvent : IEvent
    {
        public Task Notify(IEvent @event, CancellationToken cancellationToken, ServiceFactory serviceFactory, Func<IEnumerable<Func<Task>>, Task> notifier)
        {
            var handlers = serviceFactory
                .GetServices<IEventHandler<TEvent>>()
                .Select(a => new Func<Task>(() => a.Handle((TEvent) @event, cancellationToken)));

            return notifier(handlers);
        }
    }
}

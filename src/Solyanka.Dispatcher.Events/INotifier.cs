using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solyanka.Utils;

namespace Solyanka.Dispatcher.Events
{
    /// <summary>
    /// Notifier. Wrapper over notifications handlers
    /// </summary>
    public interface INotifier
    {
        /// <summary>
        /// Уведомить
        /// </summary>
        /// <param name="notification">Notification</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="serviceFactory"><see cref="ServiceFactory"/></param>
        /// <param name="notifier"><see cref="Func{TResult}"/> defining how to handle notifications</param>
        /// <returns><see cref="Task"/></returns>
        Task Notify(INotification notification, CancellationToken cancellationToken, ServiceFactory serviceFactory, Func<IEnumerable<Func<Task>>, Task> notifier);
    }
}

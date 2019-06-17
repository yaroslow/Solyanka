using System.Threading;
using System.Threading.Tasks;

namespace Solyanka.Dispatcher.Events
{
    /// <summary>
    /// Dispacther handling notifications
    /// </summary>
    public interface INotificationDispatcher
    {
        /// <summary>
        /// Notify
        /// </summary>
        /// <typeparam name="TNotification">Notification type</typeparam>
        /// <param name="notification">Notification</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        Task Notify<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : INotification;
    }
}

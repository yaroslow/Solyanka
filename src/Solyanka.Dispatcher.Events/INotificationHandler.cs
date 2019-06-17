using System.Threading;
using System.Threading.Tasks;

namespace Solyanka.Dispatcher.Events
{
    /// <summary>
    /// Notification handler
    /// </summary>
    /// <typeparam name="TNotification">Notification type</typeparam>
    public interface INotificationHandler<in TNotification> where TNotification : INotification
    {
        /// <summary>
        /// Handling
        /// </summary>
        /// <param name="notification">Notification implementing <see cref="INotification"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        Task Handle(TNotification notification, CancellationToken cancellationToken);
    }
}

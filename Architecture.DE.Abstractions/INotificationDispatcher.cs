using System.Threading;
using System.Threading.Tasks;

namespace Architecture.DE.Abstractions
{
    public interface INotificationDispatcher
    {
        Task Notify<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : INotification;
    }
}

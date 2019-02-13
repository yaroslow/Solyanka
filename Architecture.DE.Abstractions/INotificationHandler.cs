using System.Threading;
using System.Threading.Tasks;

namespace Architecture.DE.Abstractions
{
    public interface INotificationHandler<in TNotification> where TNotification : INotification
    {
        Task Handle(INotification notification, CancellationToken cancellationToken);
    }
}

using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Solyanka.ServiceBus.Microsoft.DependencyInjection;

/// <summary>
/// <see cref="IHostedService"/> that manipulates with <see cref="IBusControl"/>
/// </summary>
public class BusHostedService : IHostedService
{
    /// <summary>
    /// <see cref="IBusControl"/>
    /// </summary>
    private readonly IBusControl _busControl;

        
    /// <summary>
    /// Constructor of <see cref="BusHostedService"/>
    /// </summary>
    /// <param name="busControl"><see cref="IBusControl"/></param>
    public BusHostedService(IBusControl busControl)
    {
        _busControl = busControl;
    }

        
    /// <inheritdoc />
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return _busControl.StartAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return _busControl.StopAsync(cancellationToken);
    }
}
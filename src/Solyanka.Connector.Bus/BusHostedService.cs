using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Solyanka.Connector.Bus
{
    /// <summary>
    /// Host for bus service 
    /// </summary>
    public class BusHostedService : IHostedService
    {
        private readonly IBusControl _busControl;
        
        /// <summary/>
        public BusHostedService(IBusControl busControl)
        {
            _busControl = busControl;
        }
        
        /// <inheritdoc />
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _busControl.StartAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _busControl.StopAsync(cancellationToken);
        }
    }
}
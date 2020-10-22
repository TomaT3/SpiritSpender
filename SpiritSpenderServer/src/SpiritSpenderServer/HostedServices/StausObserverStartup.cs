using Microsoft.Extensions.Hosting;
using SpiritSpenderServer.HardwareControl;
using System.Threading;
using System.Threading.Tasks;

namespace SpiritSpenderServer.HostedServices
{
    public class StausObserverStartup : IHostedService
    {
        private readonly StatusObserver _statusObserver;

        public StausObserverStartup(StatusObserver statusObserver)
        {
            _statusObserver = statusObserver;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _statusObserver.Init();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

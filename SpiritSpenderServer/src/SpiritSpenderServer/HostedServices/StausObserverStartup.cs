namespace SpiritSpenderServer.HostedServices;

using SpiritSpenderServer.HardwareControl;

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

namespace SpiritSpenderServer.HostedServices;

using SpiritSpenderServer.API.SignalR.Hubs;

public class SignalRInformers : IHostedService
{
    private readonly AxisHubInformer _axisHubInformer;

    public SignalRInformers(AxisHubInformer axisHubInformer)
    {
        _axisHubInformer = axisHubInformer;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

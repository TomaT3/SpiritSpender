namespace SpiritSpenderServer.HostedServices;

using SpiritSpenderServer.API.SignalR.Hubs;

public class SignalRInformers : IHostedService
{
    private readonly AxisHubInformer _axisHubInformer;
    private readonly NcCommunicationHubInformer _ncCommunicationHubInformer;

    public SignalRInformers(AxisHubInformer axisHubInformer, NcCommunicationHubInformer ncCommunicationHubInformer)
    {
        _axisHubInformer = axisHubInformer;
        _ncCommunicationHubInformer = ncCommunicationHubInformer;
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

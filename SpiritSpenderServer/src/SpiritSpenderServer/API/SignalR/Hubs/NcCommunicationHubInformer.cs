namespace SpiritSpenderServer.API.SignalR.Hubs;

using Microsoft.AspNetCore.SignalR;
using NC_Communication;

public class NcCommunicationHubInformer
{
    private readonly IHubContext<NcCommunicationHub, INcCommunicationHub> _hubContext;

    public NcCommunicationHubInformer(INcCommunication ncCommunication, IHubContext<NcCommunicationHub, INcCommunicationHub> hubContext)
    {
        _hubContext = hubContext;
        ncCommunication.MessageReceived += MessageReceived;
    }

    private void MessageReceived(string message)
    {
        Task.Run(async () => await _hubContext.Clients.All.MessageReceived(message));
    }
}

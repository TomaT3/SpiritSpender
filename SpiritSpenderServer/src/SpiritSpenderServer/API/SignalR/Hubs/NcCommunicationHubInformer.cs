namespace SpiritSpenderServer.API.SignalR.Hubs;

using Microsoft.AspNetCore.SignalR;
using NC_Communication;
using Persistence.Positions;
using UnitsNet.Units;

public class NcCommunicationHubInformer
{
    private readonly IHubContext<NcCommunicationHub, INcCommunicationHub> _hubContext;

    public NcCommunicationHubInformer(INcCommunication ncCommunication, IHubContext<NcCommunicationHub, INcCommunicationHub> hubContext)
    {
        _hubContext = hubContext;
        ncCommunication.MessageReceived += MessageReceived;
    }

    private async void MessageReceived(string message)
    {
        await _hubContext.Clients.All.MessageReceived(message);
    }
}

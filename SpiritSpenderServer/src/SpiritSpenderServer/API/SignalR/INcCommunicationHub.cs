namespace SpiritSpenderServer.API.SignalR;

using UnitsNet;

public interface INcCommunicationHub
{
    Task MessageReceived(string message);
}

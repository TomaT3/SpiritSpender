using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SpiritSpenderServer.Events.SignalR
{
    public class SignalRHub : Hub, ISignalRHub
    {
        public async Task Send<TPayload>(string topic, TPayload payload)
        {
            await Clients.All.SendAsync(topic, payload);
        }
    }
}

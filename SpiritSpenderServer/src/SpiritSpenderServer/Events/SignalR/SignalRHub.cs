using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SpiritSpenderServer.Events.SignalR
{
    public class SignalRHub : Hub<ISignalRClient>, ISignalRHub
    {
        public async Task Send<TPayload>(string topic, TPayload payload)
        {
            await Clients.All.SendDummy();
        }
    }

    public interface ISignalRClient
    {
        Task SendDummy();
    }
}

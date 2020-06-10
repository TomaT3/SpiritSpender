using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SpiritSpenderServer.Events.SignalR
{
    public class SignalRHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}

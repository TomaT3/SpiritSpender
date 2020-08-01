using System.Threading.Tasks;

namespace SpiritSpenderServer.Events
{
    public interface IEventConnectionProvider
    {
        Task Send<TPayload>(string topic, TPayload payload);
    }
}
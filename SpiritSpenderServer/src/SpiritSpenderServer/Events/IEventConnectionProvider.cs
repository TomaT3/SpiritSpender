using System.Threading.Tasks;

namespace SpiritSpenderServer.Events
{
    public interface IEventConnectionProvider
    {
        public Task Send<TPayload>(string topic, TPayload payload);
    }
}
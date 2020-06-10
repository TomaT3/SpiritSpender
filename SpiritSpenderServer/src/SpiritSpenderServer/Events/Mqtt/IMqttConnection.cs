using System;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Events.Mqtt
{
    public interface IMqttConnection : IEventConnectionProvider
    {
        Task ConnectAsync();
        Task Subscribe<TPayload>(string topic, Action<TPayload> messageReceivedHandler);
        Task Unsubscribe(string topic);
        Task Publish(string topic, object payload);
    }
}
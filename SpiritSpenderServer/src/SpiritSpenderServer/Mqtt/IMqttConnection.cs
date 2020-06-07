using System;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Mqtt
{
    public interface IMqttConnection
    {
        Task ConnectAsync();
        Task Subscribe<TPayload>(string topic, Action<TPayload> messageReceivedHandler);
        Task Unsubscribe(string topic);
        Task Publish(string topic, object payload);
    }
}
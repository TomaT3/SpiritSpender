using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpiritSpenderServer.Events.Mqtt;
using SpiritSpenderServer.Events.SignalR;

namespace SpiritSpenderServer.Events
{
    public class EventConnection
    {
        private readonly IList<IEventConnectionProvider> _connectionProviders = new List<IEventConnectionProvider>();

        public EventConnection(IMqttConnection mqttConnection, ISignalRHub signalRHub)
        {
            _connectionProviders.Add(mqttConnection);
            _connectionProviders.Add(signalRHub);
        }

        public async Task Send<TPayload>(string topic, TPayload payload)
        {
            _ = topic ?? throw new ArgumentNullException(nameof(topic));
            _ = payload ?? throw new ArgumentNullException(nameof(payload));

            foreach (var provider in _connectionProviders)
            {
                await provider.Send(topic, payload);
            }
        }
    }
}

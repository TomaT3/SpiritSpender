using System;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json;
using SpiritSpenderServer.Events.Mqtt.Settings;

namespace SpiritSpenderServer.Events.Mqtt
{
    public class MqttConnection : IMqttConnection
    {
        private readonly MqttSettings _mqttSettings;
        private IManagedMqttClient _mqttClient;
        private MqttSubscribers _subscribers = new MqttSubscribers();
        

        public MqttConnection(MqttSettings mqttSettings)
        {
            _mqttSettings = mqttSettings;
        }
        
        public async Task ConnectAsync()
        {
            var options = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(1))
                .WithClientOptions(new MqttClientOptionsBuilder()
                    .WithClientId(_mqttSettings.Client.Id)
                    .WithTcpServer(_mqttSettings.Broker.Host, _mqttSettings.Broker.Port)
                    .WithCleanSession()
                    .Build())
                .Build();

            _mqttClient = new MqttFactory().CreateManagedMqttClient();
            await _mqttClient.StartAsync(options);

            StartMessageHandler();
        }

        public async Task Subscribe<TPayload>(string topic, Action<TPayload> messageReceivedHandler)
        {
            if (_subscribers.Add(topic, new MqttSubscriber<TPayload>(messageReceivedHandler)))
            {
                await _mqttClient.SubscribeAsync(new MqttTopicFilter { Topic = topic });
            }
        }

        public async Task Unsubscribe(string topic)
        {
            await _mqttClient.UnsubscribeAsync(topic);
        }

        public async Task Publish(string topic, object payload)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(JsonConvert.SerializeObject(payload))
                .Build();

            await _mqttClient.PublishAsync(message, CancellationToken.None);
        }

        private void StartMessageHandler()
        {
            _mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                var subscribers = _subscribers.GetFor(e.ApplicationMessage.Topic);

                foreach (var subscriber in subscribers)
                {
                    var payloadString = e.ApplicationMessage.ConvertPayloadToString();
                    var payloadTyped = JsonConvert.DeserializeObject(payloadString, subscriber.Type);
                    subscriber.MessageReceivedHandler(payloadTyped);
                }
            });
        }

        public async Task Send<TPayload>(string topic, TPayload payload)
        {
            await Publish(topic, payload);
        }
    }
}

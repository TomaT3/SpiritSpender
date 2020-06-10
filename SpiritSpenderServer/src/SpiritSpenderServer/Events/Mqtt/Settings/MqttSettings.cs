namespace SpiritSpenderServer.Events.Mqtt.Settings
{
    public class MqttSettings
    {
        public MqttBrokerSettings Broker { get; set; }

        public MqttClientSettings Client { get; set; }
    }
}

using System;

namespace SpiritSpenderServer.Events.Mqtt
{
    public class MqttSubscriber
    {
        public MqttSubscriber(Action<object> messageReceivedHandler, Type type)
        {
            MessageReceivedHandler = messageReceivedHandler;
            Type = type;
        }

        public Action<object> MessageReceivedHandler { get; }

        public Type Type { get; }
    }

    public class MqttSubscriber<TPayload> : MqttSubscriber
    {
        public MqttSubscriber(Action<TPayload> messageReceivedHandlerTyped) : base(o => messageReceivedHandlerTyped((TPayload)o), typeof(TPayload))
        {
            MessageReceivedHandlerTyped = messageReceivedHandlerTyped;
        }

        public Action<TPayload> MessageReceivedHandlerTyped { get; }
    }
}

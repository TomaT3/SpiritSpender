using System.Collections.Concurrent;
using System.Collections.Generic;

namespace SpiritSpenderServer.Events.Mqtt
{
    // TODO: This class is not really thread safe...
    public class MqttSubscribers
    {
        private readonly ConcurrentDictionary<string, IList<MqttSubscriber>> _subscribers = new ConcurrentDictionary<string, IList<MqttSubscriber>>();

        public bool Add<TPayload>(string topic, MqttSubscriber<TPayload> subscriber)
        {
            bool wasAdded = false;
            _subscribers.AddOrUpdate(topic, _ =>
            {
                var subscribers = new List<MqttSubscriber> {subscriber};
                wasAdded = true;
                return subscribers;
            }, (_, subscribers) =>
            {
                subscribers.Add(subscriber);
                return subscribers;
            });

            return wasAdded;
        }

        public void Remove(string topic, MqttSubscriber subscriber)
        {
            var subscribers = _subscribers.GetValueOrDefault(topic);
            if (subscribers != null && subscribers.Count > 1)
            {
                subscribers.Remove(subscriber);
            }
            else
            {
                _subscribers.TryRemove(topic, out _);
            }
        }

        public IEnumerable<MqttSubscriber> GetFor(string topic)
        {
            return _subscribers.GetValueOrDefault(topic) ?? new List<MqttSubscriber>();
        }
    }
}

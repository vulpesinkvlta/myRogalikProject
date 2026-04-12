using System;
using System.Collections.Generic;

namespace Core
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers = new();
        public void Subscribe<T>(Action<T> eventBus)
        {
            var eventType = typeof(T);
            if (!_subscribers.ContainsKey(eventType))
            {
                _subscribers[eventType] = new List<Delegate>();
            }
            _subscribers[eventType].Add(eventBus);
        }

        public void Unsubscribe<T>(Action<T> eventBus)
        {
            var eventType = typeof(T);
            if (_subscribers.ContainsKey(eventType))
            {
                _subscribers[eventType].Remove(eventBus);
                if (_subscribers[eventType].Count == 0)
                {
                    _subscribers.Remove(eventType);
                }
            }           
        }
        public void RaiseEvent<T>(T eventData)
        {
            var eventType = typeof(T);
            if (_subscribers.ContainsKey(eventType))
            {
                foreach (var subscriber in _subscribers[eventType].ToArray())
                {
                    if (subscriber is Action<T> action)
                    {
                        action(eventData);
                    }
                }
            }
        }
    }
}

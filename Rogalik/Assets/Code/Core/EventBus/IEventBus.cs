using System;

namespace Core
{
    public interface IEventBus
    {
        void Subscribe<T>(Action<T> eventBus);
        void Unsubscribe<T>(Action<T> eventBus);
        void RaiseEvent<T>(T eventData);
    }
}

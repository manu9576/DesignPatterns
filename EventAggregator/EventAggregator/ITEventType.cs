using System;

namespace EventAggregator
{
    public interface ITEventType<T>
    {
        void Publish(T eventToPublish);
        void Subscribe(Action<T> action);
        void Unsubscribe(Action<T> action);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventAggregator
{
    public class TEventType<T> : EventBase
    {
        private readonly List<WeakReference> _eventSubsribers;

        private readonly object _lockSubscriberDictionary;

        public TEventType()
        {
            _lockSubscriberDictionary = new object();
            _eventSubsribers = new List<WeakReference>();
        }

        public void Publish(T eventToPublish)
        {
            foreach (WeakReference sub in _eventSubsribers)
            {
                if (sub.IsAlive)
                {
                    Action<T> action = sub.Target as Action<T>;

                    action.Invoke(eventToPublish);
                }
            }
        }

        public void Subscribe(Action<T> action)
        {
            WeakReference weakReference = new WeakReference(action);

            if (_eventSubsribers.All(wr => (wr.Target as Action<T>) != action))
            {
                _eventSubsribers.Add(weakReference);
            }
        }

        public void Unsubscribe(Action<T> action)
        {
            if (_eventSubsribers.Any(wr => wr.Target as Action<T> == action))
            {
                _eventSubsribers.RemoveAll(wr => wr.Target as Action<T> == action);
            }
        }
    }
}
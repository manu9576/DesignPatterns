using System;
using System.Collections.Generic;

namespace EventAggregator
{
    public class EventAggregator : IEventAggregator
    {
        private Dictionary<Type, EventBase> _eventTypes = new Dictionary<Type, EventBase>();
        private static EventAggregator _instance;

        public static EventAggregator GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventAggregator();
                }

                return _instance;
            }
        }

        public T GetEvent<T>() where T : EventBase, new()
        {
            if (!_eventTypes.ContainsKey(typeof(T)))
            {
                _eventTypes.Add(typeof(T), new T());
            }

            return (T)_eventTypes[typeof(T)];
        }
    }
}
using System;
using System.Collections.Generic;

namespace EventAggregator
{
    public static class EventAggregator
    {
        private static Dictionary<Type,ITEventType<object>> _eventTypes = new Dictionary<Type, ITEventType<object>>();

        public static T GetInstance<T>() where T : ITEventType<object> , new()
        {
            if (!_eventTypes.ContainsKey(typeof(T)))
            {
                _eventTypes.Add(typeof(T), new T());
            }

            return (T)_eventTypes[typeof(T)] ;
        }
    }
}
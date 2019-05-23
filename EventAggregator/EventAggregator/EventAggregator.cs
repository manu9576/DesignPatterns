using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace EventAggregator
{
    public class EventAggregator : IEventAggregator
    {
        private static EventAggregator _instance;

        private readonly Dictionary<Type, List<WeakReference>> _eventSubsribers;

        private readonly object _lockSubscriberDictionary;

        private EventAggregator()
        {
            _lockSubscriberDictionary = new object();
            _eventSubsribers = new Dictionary<Type, List<WeakReference>>();
        }

        public static IEventAggregator Instance
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

        #region IEventAggregator Members

        /// <summary>
        /// Publish an event
        /// </summary>
        /// <typeparam name="TEventType"></typeparam>
        /// <param name="eventToPublish"></param>
        public void PublishEvent<TEventType>(TEventType eventToPublish)
        {
            Type subsriberType = typeof(ISubscriber<>).MakeGenericType(eventToPublish.GetType());

            List<WeakReference> subscribers = GetSubscriberList(subsriberType);

            List<WeakReference> subsribersToBeRemoved = new List<WeakReference>();

            foreach (WeakReference weakSubsriber in subscribers)
            {
                if (weakSubsriber.IsAlive)
                {
                    ISubscriber<TEventType> subscriber = (ISubscriber<TEventType>)weakSubsriber.Target;

                    InvokeSubscriberEvent<TEventType>(eventToPublish, subscriber);
                }
                else
                {
                    subsribersToBeRemoved.Add(weakSubsriber);
                }
            }

            if (subsribersToBeRemoved.Any())
            {
                lock (_lockSubscriberDictionary)
                {
                    foreach (WeakReference remove in subsribersToBeRemoved)
                    {
                        subscribers.Remove(remove);
                    }
                }
            }
        }

        /// <summary>
        /// Subribe to an event.
        /// </summary>
        /// <param name="subscriber"></param>
        public void SubsribeEvent(object subscriber)
        {
            lock (_lockSubscriberDictionary)
            {
                IEnumerable<Type> subsriberTypes = subscriber.GetType().GetInterfaces()
                                        .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISubscriber<>));

                WeakReference weakReference = new WeakReference(subscriber);

                foreach (Type subsriberType in subsriberTypes)
                {
                    List<WeakReference> subscribers = GetSubscriberList(subsriberType);

                    subscribers.Add(weakReference);
                }
            }
        }

        #endregion IEventAggregator Members

        private void InvokeSubscriberEvent<TEventType>(TEventType eventToPublish, ISubscriber<TEventType> subscriber)
        {
            //Synchronize the invocation of method

            SynchronizationContext syncContext = SynchronizationContext.Current;

            if (syncContext == null)
            {
                syncContext = new SynchronizationContext();
            }

            syncContext.Post(s => subscriber.OnEventHandler(eventToPublish), null);
        }

        private List<WeakReference> GetSubscriberList(Type subsriberType)
        {
            List<WeakReference> subsribersList = null;

            lock (_lockSubscriberDictionary)
            {
                bool found = this._eventSubsribers.TryGetValue(subsriberType, out subsribersList);

                if (!found)
                {
                    //First time create the list.

                    subsribersList = new List<WeakReference>();

                    this._eventSubsribers.Add(subsriberType, subsribersList);
                }
            }
            return subsribersList;
        }
    }
}
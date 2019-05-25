using System;
using System.Collections.Generic;
using System.Linq;

namespace EventAggregator
{
    public class TEventType<T> : ITEventType<T>
    {

        private readonly List<WeakReference> _eventSubsribers;

        private readonly object _lockSubscriberDictionary;

        public TEventType()
        {
            _lockSubscriberDictionary = new object();
            _eventSubsribers = new List<WeakReference>();
        }

        //#region IEventAggregator Members

        /// <summary>
        /// Publish an event
        /// </summary>
        /// <typeparam name="TEventType"></typeparam>
        /// <param name="eventToPublish"></param>
        //public void PublishEvent<TEventType>(TEventType eventToPublish)
        //{
        //    Type subsriberType = typeof(ISubscriber<>).MakeGenericType(eventToPublish.GetType());

        //    List<WeakReference> subscribers = GetSubscriberList(subsriberType);

        //    List<WeakReference> subsribersToBeRemoved = new List<WeakReference>();

        //    foreach (WeakReference weakSubsriber in subscribers)
        //    {
        //        if (weakSubsriber.IsAlive)
        //        {
        //            ISubscriber<TEventType> subscriber = (ISubscriber<TEventType>)weakSubsriber.Target;

        //            InvokeSubscriberEvent<TEventType>(eventToPublish, subscriber);
        //        }
        //        else
        //        {
        //            subsribersToBeRemoved.Add(weakSubsriber);
        //        }
        //    }

        //    if (subsribersToBeRemoved.Any())
        //    {
        //        lock (_lockSubscriberDictionary)
        //        {
        //            foreach (WeakReference remove in subsribersToBeRemoved)
        //            {
        //                subscribers.Remove(remove);
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Subribe to an event.
        /// </summary>
        /// <param name="subscriber"></param>
        //public void SubsribeEvent(object subscriber)
        //{
        //    lock (_lockSubscriberDictionary)
        //    {
        //        IEnumerable<Type> subsriberTypes = subscriber.GetType().GetInterfaces()
        //                                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISubscriber<>));

        //        WeakReference weakReference = new WeakReference(subscriber);

        //        foreach (Type subsriberType in subsriberTypes)
        //        {
        //            List<WeakReference> subscribers = GetSubscriberList(subsriberType);

        //            subscribers.Add(weakReference);
        //        }
        //    }
        //}

        //#endregion IEventAggregator Members

        //private void InvokeSubscriberEvent<TEventType>(TEventType eventToPublish, ISubscriber<TEventType> subscriber)
        //{
        //    Synchronize the invocation of method

        //    SynchronizationContext syncContext = SynchronizationContext.Current;

        //    if (syncContext == null)
        //    {
        //        syncContext = new SynchronizationContext();
        //    }

        //    syncContext.Post(s => subscriber.OnEventHandler(eventToPublish), null);
        //}

        //private List<WeakReference> GetSubscriberList(Type subsriberType)
        //{
        //    List<WeakReference> subsribersList = null;

        //    lock (_lockSubscriberDictionary)
        //    {
        //        bool found = this._eventSubsribers.TryGetValue(subsriberType, out subsribersList);

        //        if (!found)
        //        {
        //            First time create the list.

        //            subsribersList = new List<WeakReference>();

        //            this._eventSubsribers.Add(subsriberType, subsribersList);
        //        }
        //    }
        //    return subsribersList;
        //}


        public void Publish(T eventToPublish)
        {
            foreach(var sub in _eventSubsribers)
            {
                if(sub.IsAlive)
                {
                    var action = sub.Target as Action<T>;

                    action.Invoke(eventToPublish);
                }
            }
        }

        public void Subscribe(Action<T> action)
        {
            var weakReference  = new WeakReference(action);

            _eventSubsribers.Add(weakReference);


        }

        public void Unsubscribe(Action<T> action)
        {
            if (_eventSubsribers.Any(wr => wr.Target == action))
            {
                _eventSubsribers.RemoveAll(wr => wr.Target == action);
            }
        }
    }
}

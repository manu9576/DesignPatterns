using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace EventAggregator
{
    public class TEventType<T> : EventBase
    {
        private readonly List<Tuple<WeakReference, ThreadOptions>> _eventSubsribers;

        private readonly object _lockSubscriberDictionary;

        public TEventType()
        {
            _lockSubscriberDictionary = new object();
            _eventSubsribers = new List<Tuple<WeakReference,ThreadOptions>>();
        }

        public void Publish(T eventToPublish)
        {
            foreach (var sub in _eventSubsribers)
            {
                if (sub.Item1.IsAlive)
                {
                    Action<T> action = sub.Item1.Target as Action<T>;

                    switch(sub.Item2)
                    {
                        case ThreadOptions.PublisherThread:
                            action.Invoke(eventToPublish);
                            break;

                        case ThreadOptions.UIThread:
                            Application.Current.Dispatcher.BeginInvoke(action, eventToPublish);
                            break;

                        case ThreadOptions.BackgroundThread:

                            var bw = new BackgroundWorker();
                            bw.DoWork += (_,__) =>
                            {
                                action.Invoke(eventToPublish);
                            };
                            bw.RunWorkerAsync();
                            break;
                    }
                }
            }
        }

        public void Subscribe(Action<T> action, ThreadOptions threadOption = ThreadOptions.PublisherThread)
        {
            WeakReference weakReference = new WeakReference(action);

            if (!_eventSubsribers.Any(wr => (wr.Item1.Target as Action<T>) == action))
            {
                _eventSubsribers.Add(new Tuple<WeakReference,ThreadOptions>( weakReference, threadOption));
            }
        }

        public void Unsubscribe(Action<T> action)
        {
            var weakReferenceToRemove = _eventSubsribers.FirstOrDefault(wr => wr.Item1.Target as Action<T> == action);

            if (weakReferenceToRemove != null)
            {
                _eventSubsribers.Remove(weakReferenceToRemove);
            }
        }
    }
}
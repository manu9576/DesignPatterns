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
            _eventSubsribers = new List<Tuple<WeakReference, ThreadOptions>>();

            if (null == Application.Current)
            {
                new Application();
            }
        }

        public void Publish(T eventToPublish)
        {
            foreach (Tuple<WeakReference, ThreadOptions> sub in _eventSubsribers)
            {
                if (sub.Item1.IsAlive)
                {
                    Action<T> action = sub.Item1.Target as Action<T>;

                    switch (sub.Item2)
                    {
                        case ThreadOptions.PublisherThread:
                            action.Invoke(eventToPublish);
                            break;

                        case ThreadOptions.UIThread:
                            Application.Current.Dispatcher.Invoke(action, eventToPublish);
                            break;

                        case ThreadOptions.BackgroundThread:
                            BackgroundWorker bw = new BackgroundWorker();
                            bw.DoWork += (_, __) =>
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
                _eventSubsribers.Add(new Tuple<WeakReference, ThreadOptions>(weakReference, threadOption));
            }
        }

        public void Unsubscribe(Action<T> action)
        {
            Tuple<WeakReference, ThreadOptions> weakReferenceToRemove = _eventSubsribers.FirstOrDefault(wr => wr.Item1.Target as Action<T> == action);

            if (weakReferenceToRemove != null)
            {
                _eventSubsribers.Remove(weakReferenceToRemove);
            }
        }
    }
}
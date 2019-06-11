using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventAggregator
{
    public enum ThreadOptions
    {
        PublisherThread,  // Use this setting to receive the event on the publishers' thread. This is the default setting.
        BackgroundThread, // Use this setting to asynchronously receive the event on a .NET Framework thread-pool thread.
        UIThread          // Use this setting to receive the event on UI's thead
    }
}

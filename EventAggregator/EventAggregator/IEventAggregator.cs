/*
* Gautham Prabhu K 2014
* Copyright (c) 2014  gautham.prabhu.se@gmail.com
* All rights reserved.
* No warranty of any kind implied or otherwise.
* 
*/

using System;

namespace EventAggregator
{
    public interface IEventAggregator
    {        
        void PublishEvent<TEventType>(TEventType eventToPublish);

        void SubsribeEvent(Object subscriber);
    }
}

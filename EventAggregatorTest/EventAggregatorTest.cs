using EventAggregator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EventAggregatorTest
{
    [TestClass]
    public class EventAggregatorTest
    {
        [TestMethod]
        public void PublishesEventToSubscriber()
        {
            string response = string.Empty;

            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Subscribe((str) => response = str.ToString());

            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Publish("Test");

            Assert.AreEqual("Test", response);
        }

        [TestMethod]
        public void DoesntPublishEventToUnsubscriberAction()
        {
            string response = string.Empty;

            Action<object> action = ((str) => response = str.ToString());

            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Subscribe(action);
            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Unsubscribe(action);

            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Publish("Test");

            Assert.AreEqual(string.Empty, response);
        }

        private class StringEvent : TEventType<string> { }
    }
}
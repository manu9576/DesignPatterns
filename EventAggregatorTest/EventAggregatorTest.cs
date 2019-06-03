using EventAggregator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace EventAggregatorTest
{
    [TestClass]
    public class EventAggregatorTest
    {
        private class StringEvent : TEventType<string> { }

        private class ObjectEventArgs
        {
            public string Message { get; set; }
            public List<int> ListInt { get; set; }
        }
        private class ObjectEvent : TEventType<ObjectEventArgs> { }

        [TestMethod]
        public void PublishdEventRaisesAction()
        {
            string response = string.Empty;

            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Subscribe((str) => response = str.ToString());

            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Publish("Test");

            Assert.AreEqual("Test", response);
        }

        [TestMethod]
        public void PublishdEventRaisesActions()
        {
            string response1 = string.Empty;
            string response2 = string.Empty;
            string response3 = string.Empty;

            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Subscribe((str) => response1 = str.ToString());
            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Subscribe((str) => response2 = str.ToString());
            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Subscribe((str) => response3 = str.ToString());

            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Publish("Test");

            Assert.AreEqual("Test", response1);
            Assert.AreEqual("Test", response2);
            Assert.AreEqual("Test", response3);
        }

        [TestMethod]
        public void PublishdEventsRaisesActions()
        {
            string response1 = string.Empty;
            string response2 = string.Empty;
            string response3 = string.Empty;

            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Subscribe((str) => response1 = str.ToString());        
            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Publish("Test");


            Assert.AreEqual("Test", response1);
            Assert.AreEqual(string.Empty, response2);
            Assert.AreEqual(string.Empty, response3);

            response1 = string.Empty;
            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Subscribe((str) => response2 = str.ToString());
            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Publish("Test");

            Assert.AreEqual("Test", response1);
            Assert.AreEqual("Test", response2);
            Assert.AreEqual(string.Empty, response3);


            response1 = string.Empty;
            response2 = string.Empty;
            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Subscribe((str) => response3 = str.ToString());
            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Publish("Test");

            Assert.AreEqual("Test", response1);
            Assert.AreEqual("Test", response2);
            Assert.AreEqual("Test", response3);
        }

        [TestMethod]
        public void DoesntRaiseActionIfUnsubscribe()
        {
            string response = string.Empty;

            Action<string> action = ((str) => response = str);

            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Subscribe(action);
            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Unsubscribe(action);

            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Publish("Test");

            Assert.AreEqual(string.Empty, response);
        }

        [TestMethod]
        public void RaisesActionWithObject()
            {
            ObjectEventArgs response = null;

            ObjectEventArgs raisedEvent = new ObjectEventArgs
            {
                Message = "Hello",
                ListInt = new List<int> { 1, 2, 3, 4 }
            };

            EventAggregator.EventAggregator.GetInstance.GetEvent<ObjectEvent>().Subscribe((obj) => response = obj);
            
            EventAggregator.EventAggregator.GetInstance.GetEvent<ObjectEvent>().Publish(raisedEvent);

            Assert.AreSame(raisedEvent, response);
        }
    }
}
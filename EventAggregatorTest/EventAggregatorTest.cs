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

        private class TestObject
        {
            public string String { get; set; }

            public void Method(string str)
            {
                String = str;
            }
        }

        [TestMethod]
        public void PublishdEventRaisesAction()
        {
            string response = string.Empty;

            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Subscribe((str) => response = str.ToString());

            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Publish("Test");

            Assert.AreEqual("Test", response);
        }

        [TestMethod]
        public void PublishesEventWithNoSubscriberDoesntRaiseException()
        {
            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Publish("Test");
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
        public void DoesntRaiseActionIfActionIsUnsubscribed()
        {
            string response = string.Empty;

            Action<string> action = ((str) => response = str);

            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Subscribe(action);
            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Unsubscribe(action);
            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Publish("Test");

            Assert.AreEqual(string.Empty, response);
        }

        [TestMethod]
        public void UnsubscribesActionThatNotBeSubscribeDoesNotThrowException()
        {
            string response = string.Empty;
            Action<string> action = ((str) => response = str);

            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Unsubscribe(action);
        }

        [TestMethod]
        public void ActionCanBeSubscribeOnlyOneTime()
        {
            int count= 0;
            Action<string> action = ((str) => count++);

            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Subscribe(action);
            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Subscribe(action);
            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Publish("string");

            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void RaisedEventWithObjectIsPublished()
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

        [TestMethod]
        public void SubscribedObjectDontKeepAliveRef()
        {
            TestObject testObject = new TestObject();

            WeakReference wk = new WeakReference(new TestObject());

            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Subscribe((wk.Target as TestObject).Method);

            Assert.AreNotEqual("Test", (wk.Target as TestObject).String);

            EventAggregator.EventAggregator.GetInstance.GetEvent<StringEvent>().Publish("Test");

            Assert.AreEqual("Test", (wk.Target as TestObject).String);
            Assert.IsTrue(wk.IsAlive);

            GC.Collect();

            Assert.IsFalse(wk.IsAlive);
        }
    }
}
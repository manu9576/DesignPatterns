using System;
using EventAggregator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventAggregatorTest
{
    [TestClass]
    public class EventAggregatorTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string response =string.Empty;

            EventAggregator.EventAggregator.GetInstance<StringEvent>().Subscribe((str) => response = str.ToString());

            EventAggregator.EventAggregator.GetInstance<StringEvent>().Publish("Test");

            Assert.AreEqual("Test", response);

        }


        private class StringEvent : TEventType<object> { }
        
    }
}

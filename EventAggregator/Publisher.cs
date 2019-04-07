using System;

namespace DesignPatterns
{
    public class Publisher<T> : IPublisher<T>
    {
        //Defined datapublisher event
        public event EventHandler<MessageArgument<T>> DataPublisher;

        private void OnDataPublisher(MessageArgument<T> args)
        {
            DataPublisher?.Invoke(this, args);
        }


        public void PublishData(T data)
        {
            MessageArgument<T> message = (MessageArgument<T>)Activator.CreateInstance(typeof(MessageArgument<T>), new object[] { data });
            OnDataPublisher(message);
        }
    }
}

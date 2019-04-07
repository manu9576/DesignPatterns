using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class Program
    {

        static void Main(string[] args)
        {
            IPublisher<int> IntPublisher = new Publisher<int>();//create publisher of type integer

            Subscriber<int> IntSublisher1 = new Subscriber<int>(IntPublisher);//subscriber 1 subscribe to integer publisher
            IntSublisher1.Publisher.DataPublisher += Publisher_DataPublisher1;//event method to listen publish data

            Subscriber<int> IntSublisher2 = new Subscriber<int>(IntPublisher);//subscriber 2 subscribe to interger publisher
            IntSublisher2.Publisher.DataPublisher += Publisher_DataPublisher2;//event method to listen publish data

            IntPublisher.PublishData(10); // publisher publish message

            Console.ReadKey();
        }

        static void Publisher_DataPublisher1(object sender, MessageArgument<int> e)
        {
            Console.WriteLine("Subscriber 1 : " + e.Message);
        }

        static void Publisher_DataPublisher2(object sender, MessageArgument<int> e)
        {
            Console.WriteLine("Subscriber 2 : " + e.Message);
        }
    }
}

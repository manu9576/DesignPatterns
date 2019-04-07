using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class Subscriber<T>
    {
        public IPublisher<T> Publisher { get; private set; }

        public Subscriber(IPublisher<T> publisher)
        {
            Publisher = publisher;
        }
    }
}

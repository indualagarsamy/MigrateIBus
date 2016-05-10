using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace NsbHostUsingV5Core
{
    public class Bootstrapper : IWantToRunWhenBusStartsAndStops
    {
        public ISomeComponentUsedOutsideMessageHandlingThatUsesTheBus SomeComponentUsedOutsideMessageHandlingThatUsesTheBus { get; set; }
        public void Start()
        {
            Console.WriteLine("Endpoint bootstrapper - calling custom component");
            SomeComponentUsedOutsideMessageHandlingThatUsesTheBus.DoSomeDomainBehavior();
        }

        public void Stop()
        {
            
        }
    }
}

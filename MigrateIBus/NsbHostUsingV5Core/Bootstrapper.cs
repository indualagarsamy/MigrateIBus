using System;
using NServiceBus;

namespace NsbHostUsingV5Core
{
    public class Bootstrapper : IWantToRunWhenBusStartsAndStops
    {
        public ISomeComponent SomeComponent { get; set; }
        public void Start()
        {
            Console.WriteLine("Endpoint bootstrapper - calling custom component");
            SomeComponent.DoSomeDomainBehavior();
        }

        public void Stop()
        {
            
        }
    }
}

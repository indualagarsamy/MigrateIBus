using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace NsbHostUsingV6Core
{
    public class Boostrapper : IWantToRunWhenEndpointStartsAndStops
    {
        public Task Start(IMessageSession session)
        {
            return Task.FromResult(0);
        }

        public Task Stop(IMessageSession session)
        {
            return Task.FromResult(0);
        }
    }
}

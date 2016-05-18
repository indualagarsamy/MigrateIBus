using System.Threading.Tasks;
using NServiceBus;

namespace NsbHostUsingV6Core
{
    using Ninject;

    public class Bootstrapper : IWantToRunWhenEndpointStartsAndStops
    {
        public Task Start(IMessageSession session)
        {
            RegisterMessageSession(session);

            var component = EndpointConfig.Kernel.Get<ISomeComponent>();
            component.DoSomeDomainBehavior();

            return Task.FromResult(0);
        }

        private static void RegisterMessageSession(IMessageSession session)
        {
            EndpointConfig.Kernel.Bind<IMessageSession>().ToConstant(session);
            EndpointConfig.Kernel.Bind<ISomeComponent>().ToConstant(new SomeComponentThatUsesTheBus());
        }

        public Task Stop(IMessageSession session)
        {
            return Task.FromResult(0);
        }
    }
}

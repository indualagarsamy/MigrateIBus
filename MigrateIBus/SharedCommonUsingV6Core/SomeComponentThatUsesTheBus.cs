using System;
using System.Threading.Tasks;
using Ninject;
using NServiceBus;

public class SomeComponentThatUsesTheBus : ISomeComponent
{
    [Inject]
    public IMessageSession Bus { get; set; }

    public void DoSomeDomainBehavior()
    {
        Console.WriteLine("Inside DoSomeDomainBehavior - use the bus to send a command");
        //TODO: Will interface methods need to be changed to float the appropriate context as parameters?
        //TODO: What if IEndpointInstance is used? What if a Domain method had reply logic and previouly Bus.Reply was used. What then?
        Bus.SendLocal(new DoSomething());
    }
}

public interface ISomeComponent
{
    void DoSomeDomainBehavior();
}

public class DoSomething : ICommand
{
}

public class DoSomethingHandler : IHandleMessages<DoSomething>
{
    public Task Handle(DoSomething message, IMessageHandlerContext context)
    {
        Console.WriteLine("DoSomething has been invoked");
        return Task.FromResult(0);
    }
}


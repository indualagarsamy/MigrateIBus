using System;
using NServiceBus;

public class SomeComponentThatUsesTheBus : ISomeComponent
{
    public IBus Bus { get; set; }

    public void DoSomeDomainBehavior()
    {
        Console.WriteLine("Inside DoSomeDomainBehavior - use the bus to send a command");
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
    public void Handle(DoSomething message)
    {
        Console.WriteLine("DoSomething has been invoked");
    }
}


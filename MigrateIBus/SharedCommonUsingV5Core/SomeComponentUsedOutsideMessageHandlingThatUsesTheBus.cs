using System;
using NServiceBus;

public class SomeComponentUsedOutsideMessageHandlingThatUsesTheBus : ISomeComponentUsedOutsideMessageHandlingThatUsesTheBus
{
    public IBus Bus { get; set; }

    public void DoSomeDomainBehavior()
    {
        Console.WriteLine("Inside DoSomeDomainBehavior - use the bus to send a command");
        Bus.SendLocal(new DoSomething());
    }
}

public interface ISomeComponentUsedOutsideMessageHandlingThatUsesTheBus
{
    void DoSomeDomainBehavior();
}

public class AnotherComponentUsedOutsideMessageHandling
{
    private ISomeComponentUsedOutsideMessageHandlingThatUsesTheBus _dependency;

    public AnotherComponentUsedOutsideMessageHandling(ISomeComponentUsedOutsideMessageHandlingThatUsesTheBus dependency)
    {
        _dependency = dependency;
    }

    public void Start()
    {
        _dependency.DoSomeDomainBehavior();
    }

    public void Stop()
    {
    }
}

public class SomeComponentUsedInsideMessageHandlingThatUsesTheBus : ISomeComponentUsedInsideMessageHandlingThatUsesTheBus
{
    public IBus Bus { get; set; }

    public void DoSomeDomainBehavior()
    {
        Console.WriteLine("Inside DoSomeDomainBehavior - use the bus to send a command");
        Bus.SendLocal(new DoSomething());
    }
}

public interface ISomeComponentUsedInsideMessageHandlingThatUsesTheBus
{
    void DoSomeDomainBehavior();
}

public class DoSomething : ICommand
{
}

public class DoSomethingHandler : IHandleMessages<DoSomething>
{
    private ISomeComponentUsedInsideMessageHandlingThatUsesTheBus _dependency;

    public DoSomethingHandler(ISomeComponentUsedInsideMessageHandlingThatUsesTheBus dependency)
    {
        _dependency = dependency;
    }

    public void Handle(DoSomething message)
    {
        Console.WriteLine("DoSomething has been invoked");
        _dependency.DoSomeDomainBehavior();
    }
}


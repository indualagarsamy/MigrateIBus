﻿using System;
using System.Threading.Tasks;
using NServiceBus;

public class SomeComponentUsedOutsideMessageHandlingThatUsesTheBus : ISomeComponentUsedOutsideMessageHandlingThatUsesTheBus
{
    public IMessageSession Session { get; set; }

    public async Task DoSomeDomainBehavior()
    {
        Console.WriteLine("Inside DoSomeDomainBehavior - use the bus to send a command");
        await Session.SendLocal(new DoSomething());
    }
}

public interface ISomeComponentUsedOutsideMessageHandlingThatUsesTheBus
{
    Task DoSomeDomainBehavior();
}

public class AnotherComponentUsedOutsideMessageHandling
{
    private ISomeComponentUsedOutsideMessageHandlingThatUsesTheBus _dependency;

    public AnotherComponentUsedOutsideMessageHandling(ISomeComponentUsedOutsideMessageHandlingThatUsesTheBus dependency)
    {
        _dependency = dependency;
    }

    public async Task Start()
    {
        await _dependency.DoSomeDomainBehavior();
    }

    public void Stop()
    {
    }
}

public class SomeComponentUsedInsideMessageHandlingThatUsesTheBus : ISomeComponentUsedInsideMessageHandlingThatUsesTheBus
{
    public async Task DoSomeDomainBehavior(IMessageHandlerContext context)
    {
        Console.WriteLine("Inside DoSomeDomainBehavior - use the bus to send a command");
        await context.SendLocal(new DoSomething());
    }
}

public interface ISomeComponentUsedInsideMessageHandlingThatUsesTheBus
{
    Task DoSomeDomainBehavior(IMessageHandlerContext context);
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

    public async Task Handle(DoSomething message, IMessageHandlerContext context)
    {
        Console.WriteLine("DoSomething has been invoked");

        await _dependency.DoSomeDomainBehavior(context);
    }
}


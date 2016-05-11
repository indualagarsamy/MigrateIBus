namespace NsbHostUsingV6Core
{
    using Ninject;
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint
    {
        internal static StandardKernel Kernel = new StandardKernel();
        
        public void Customize(EndpointConfiguration endpointConfiguration)
        {
            //TODO: NServiceBus provides multiple durable storage options, including SQL Server, RavenDB, and Azure Storage Persistence.
            // Refer to the documentation for more details on specific options.
            endpointConfiguration.UsePersistence<InMemoryPersistence>();

            // NServiceBus will move messages that fail repeatedly to a separate "error" queue. We recommend
            // that you start with a shared error queue for all your endpoints for easy integration with ServiceControl.
            endpointConfiguration.SendFailedMessagesTo("error");

            // NServiceBus will store a copy of each successfully process message in a separate "audit" queue. We recommend
            // that you start with a shared audit queue for all your endpoints for easy integration with ServiceControl.
            endpointConfiguration.AuditProcessedMessagesTo("audit");

            endpointConfiguration.UseContainer<NinjectBuilder>(c => c.ExistingKernel(Kernel));
        }
    }
}

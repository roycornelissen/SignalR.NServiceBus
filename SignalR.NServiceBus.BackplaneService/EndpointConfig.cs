using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Persistence;
using NServiceBus.Persistence.Legacy;

namespace SignalR.NServiceBus.BackplaneService
{
    /// <summary>
    /// Configuration for the backplane NServiceBus process.
    /// 
    /// You can host the SignalR.NServiceBus.Backplane message handler inside your own endpoint.
    /// This endpoint serves a an example.
    /// </summary>
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration busConfiguration)
        {
            busConfiguration.UseTransport<MsmqTransport>();
            busConfiguration.UsePersistence<InMemoryPersistence>();
            busConfiguration.UsePersistence<MsmqPersistence, StorageType.Subscriptions>();
            busConfiguration.EnableInstallers();
        }
    }
}
using NServiceBus;
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
        public void Customize(EndpointConfiguration cfg)
        {
            cfg.UseTransport<MsmqTransport>();
            cfg.UsePersistence<InMemoryPersistence>();
            cfg.UsePersistence<MsmqPersistence, StorageType.Subscriptions>();
            cfg.EnableInstallers();
        }
    }
}
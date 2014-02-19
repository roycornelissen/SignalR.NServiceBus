using NServiceBus;

namespace SignalR.NServiceBus.BackplaneService 
{
    /// <summary>
    /// Configuration for the backplane NServiceBus process.
    /// </summary>
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher
    {
    }
}
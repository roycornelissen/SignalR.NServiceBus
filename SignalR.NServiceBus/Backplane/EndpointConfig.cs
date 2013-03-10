using NServiceBus;

namespace SignalR.NServiceBus.Backplane 
{
    /// <summary>
    /// Configuration for the backplane NServiceBus process.
    /// </summary>
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher
    {
    }
}
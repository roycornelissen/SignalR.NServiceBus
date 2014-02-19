using NServiceBus;

namespace SignalR.NServiceBus.BackplaneService 
{
    /// <summary>
    /// Configuration for the backplane NServiceBus process.
    /// 
    /// You can host the SignalR.NServiceBus.Backplane message handler inside your own endpoint.
    /// This endpoint serves a an example.
    /// </summary>
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher
    {
    }
}
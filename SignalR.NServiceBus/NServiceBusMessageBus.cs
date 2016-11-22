using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Messaging;
using SignalR.NServiceBus.Messages;
using NServiceBus;

namespace SignalR.NServiceBus
{
    /// <summary>
    /// The SignalR messagebus that uses NServiceBus as a backplane.
    /// </summary>
    public class NServiceBusMessageBus : ScaleoutMessageBus
    {
        private static IEndpointInstance _endpointInstance;

        public NServiceBusMessageBus(IDependencyResolver resolver, IEndpointInstance endpointInstance, ScaleoutConfiguration configuration)
            : base(resolver, configuration)
        {
            _endpointInstance = endpointInstance;

            // By default, there is only 1 stream in this NServiceBus backplane, and we'll open it here
            Open(0);
        }

        protected override Task Send(int streamIndex, IList<Message> messages)
        {
            var msg = new ScaleoutMessage(messages);
            return _endpointInstance.Send<DistributeMessages>(m => { m.Payload = msg.ToBytes(); m.StreamIndex = streamIndex; });
        }

        internal new void OnReceived(int streamIndex, ulong id, ScaleoutMessage messages)
        {
            base.OnReceived(streamIndex, id, messages);
        }
    }
}

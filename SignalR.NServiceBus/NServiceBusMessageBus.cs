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
        private static IBus _bus;

        public NServiceBusMessageBus(IDependencyResolver resolver, IBus busInstance, ScaleoutConfiguration configuration)
            : base(resolver, configuration)
        {
            _bus = busInstance;

            // By default, there is only 1 stream in this NServiceBus backplane, and we'll open it here
            Open(0);
        }

        protected override Task Send(int streamIndex, IList<Message> messages)
        {
            return Task.Factory.StartNew(() =>
                {
                    var msg = new ScaleoutMessage(messages);
                    _bus.Send<DistributeMessages>(m => { m.Payload = msg.ToBytes(); m.StreamIndex = streamIndex; });
                });
        }

        internal new void OnReceived(int streamIndex, ulong id, ScaleoutMessage messages)
        {
            base.OnReceived(streamIndex, id, messages);
        }
    }
}

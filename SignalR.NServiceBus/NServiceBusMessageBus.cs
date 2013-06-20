using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Messaging;
using SignalR.NServiceBus.Endpoint;
using SignalR.NServiceBus.Messages;
using Newtonsoft.Json;
using NServiceBus;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace SignalR.NServiceBus
{
    /// <summary>
    /// The SignalR messagebus that uses NServiceBus as a backplane.
    /// </summary>
    public class NServiceBusMessageBus : ScaleoutMessageBus
    {
        internal static IBus Bus;

        public NServiceBusMessageBus(IDependencyResolver resolver, IBus busInstance, ScaleoutConfiguration configuration)
            : base(resolver, configuration)
        {
            Bus = busInstance;
            Configure.Instance.Configurer.ConfigureComponent<Receiver>(DependencyLifecycle.InstancePerCall)
                .ConfigureProperty((r) => r.SignalRMessageBus, this);

            // By default, there is only 1 stream in this NServiceBus backplane, and we'll open it here
            Open(0);
        }

        protected override Task Send(int streamIndex, IList<Message> messages)
        {
            return Task.Factory.StartNew(() =>
                {
                    ScaleoutMessage msg = new ScaleoutMessage(messages);
                    Bus.Send<DistributeMessages>(m => { m.Payload = msg.ToBytes(); m.StreamIndex = streamIndex; });
                });
        }

        new internal void OnReceived(int streamIndex, ulong id, ScaleoutMessage messages)
        {
            base.OnReceived(streamIndex, id, messages);
        }
    }
}

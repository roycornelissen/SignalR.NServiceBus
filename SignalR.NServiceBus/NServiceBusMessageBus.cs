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
    public class NServiceBusMessageBus : ScaleoutMessageBus
    {
        internal static IBus Bus;
        private readonly TaskQueue _publishQueue = new TaskQueue();

        public NServiceBusMessageBus(IDependencyResolver resolver, IBus busInstance)
            : base(resolver)
        {
            Bus = busInstance;
            Configure.Instance.Configurer.ConfigureComponent<Receiver>(DependencyLifecycle.InstancePerCall)
                .ConfigureProperty((r) => r.SignalRMessageBus, this);
        }

        internal void OnReceived(ulong id, Message[] messages)
        {
            _publishQueue.Enqueue(() => OnReceived("SignalR.NServiceBus", id, messages));
        }

        protected override Task Send(IList<Message> messages)
        {
            return Task.Factory.StartNew(() =>
                {
                    Bus.Send<DistributeMessages>(m => m.Payload = JsonConvert.SerializeObject(messages.ToArray()));
                });
        }
    }
}

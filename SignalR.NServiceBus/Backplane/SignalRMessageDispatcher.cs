using SignalR.NServiceBus.Messages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SignalR.NServiceBus.Backplane
{
    /// <summary>
    /// Handler that will relay the incoming message to all SignalR instances.
    /// </summary>
    public class SignalRMessageDispatcher: IHandleMessages<DistributeMessages>
    {
        private static ulong _payloadId = 0;
        private static object _lockHandle = new object();

        public IBus Bus { get; set; }

        public void Handle(DistributeMessages message)
        {
            var evt = new MessagesAvailable() { Payload = message.Payload };
            lock (_lockHandle)
            {
                _payloadId++;
                evt.PayloadId = _payloadId;
            }

            Bus.Publish(evt);
        }
    }
}

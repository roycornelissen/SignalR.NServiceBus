using SignalR.NServiceBus.Messages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace SignalR.NServiceBus.Backplane
{
    /// <summary>
    /// Handler that will relay the incoming message to all SignalR instances.
    /// </summary>
    public class SignalRMessageDispatcher : IHandleMessages<DistributeMessages>
    {
        private static long _payloadId = 0;

        public IBus Bus { get; set; }

        public void Handle(DistributeMessages message)
        {
            var evt = new MessagesAvailable() { Payload = message.Payload, StreamIndex = message.StreamIndex, PayloadId = (ulong) Interlocked.Increment(ref _payloadId) };
            Console.WriteLine("Got a message to distribute " + evt.PayloadId);
            Bus.Publish(evt);
        }
    }
}

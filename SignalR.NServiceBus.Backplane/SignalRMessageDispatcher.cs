using SignalR.NServiceBus.Messages;
using NServiceBus;
using System.Threading;
using System.Threading.Tasks;

namespace SignalR.NServiceBus.Backplane
{
    /// <summary>
    /// Handler that will relay the incoming message to all SignalR instances.
    /// </summary>
    public class SignalRMessageDispatcher : IHandleMessages<DistributeMessages>
    {
        private static long _payloadId = 0;

        public Task Handle(DistributeMessages message, IMessageHandlerContext context)
        {
            var evt = new MessagesAvailable { Payload = message.Payload, StreamIndex = message.StreamIndex, PayloadId = (ulong) Interlocked.Increment(ref _payloadId) };

            return context.Publish(evt);
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Messaging;
using SignalR.NServiceBus.Messages;
using NServiceBus;

namespace SignalR.NServiceBus.Endpoint
{
    /// <summary>
    /// This handler will receive messages from the backplane and hand them off to SignalR.
    /// </summary>
    public class Receiver: IHandleMessages<MessagesAvailable>
    {
        private readonly NServiceBusMessageBus signalRMessageBus;

        public Receiver(NServiceBusMessageBus signalRMessageBus)
        {
            this.signalRMessageBus = signalRMessageBus;
        }

        public Task Handle(MessagesAvailable message, IMessageHandlerContext context)
        {
            var messages = ScaleoutMessage.FromBytes(message.Payload);

            if (signalRMessageBus != null)
            {
                signalRMessageBus.OnReceived(message.StreamIndex, message.PayloadId, messages);
            }
            return Task.FromResult(0);
        }
    }
}

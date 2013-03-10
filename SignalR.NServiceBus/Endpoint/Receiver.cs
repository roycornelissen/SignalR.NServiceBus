using Microsoft.AspNet.SignalR.Messaging;
using SignalR.NServiceBus.Messages;
using Newtonsoft.Json;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.NServiceBus.Endpoint
{
    public class Receiver: IHandleMessages<MessagesAvailable>
    {
        public NServiceBusMessageBus SignalRMessageBus { get; set; }

        public void Handle(MessagesAvailable message)
        {
            var messages = JsonConvert.DeserializeObject<Message[]>(message.Payload);

            if (SignalRMessageBus != null)
            {
                SignalRMessageBus.OnReceived(message.PayloadId, messages);
            }
        }
    }
}

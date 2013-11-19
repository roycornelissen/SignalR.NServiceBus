using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace MessageShooter.Messages.Events
{
    public class ChatMessage : IEvent
    {
        public string ClientId {get; set;}
        public string Name { get; set; }
        public string Message { get; set; }

    }
}

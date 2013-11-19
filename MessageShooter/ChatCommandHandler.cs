using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageShooter
{
    class ChatCommandHandler : IHandleMessages<MessageShooter.Messages.Commands.ChatCommand>
    {

        public IBus Bus { get; set; }
        public void Handle(MessageShooter.Messages.Commands.ChatCommand message)
        {
            Console.WriteLine("I got a message from client: " + message.ClientId);
            Bus.Publish(new MessageShooter.Messages.Events.ChatMessage()
            {
                ClientId = message.ClientId,
                Name = "MessageShooter",
                Message = " A messge from Message Shooter."
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using NServiceBus;

namespace DemoWebApp
{
    public class ChatHub : Hub, IHandleMessages<MessageShooter.Messages.Events.ChatMessage>
    {

        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
           
            
            //Send the ChatCommand to the MessageShooter
            Startup.Bus.Send(new MessageShooter.Messages.Commands.ChatCommand()
            {
                ClientId = Context.ConnectionId
            });
        }

        public void Handle(MessageShooter.Messages.Events.ChatMessage message)
        {
            Console.WriteLine(message.Name + " received " + message.Message);
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();

            context.Clients.Client(message.ClientId).addNewMessageToPage(message.Name, message.Message);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Installation.Environments;

namespace MessageShooter
{
    class Program
    {
        public static IBus Bus;
        static void Main(string[] args)
        {

            //Bus = Configure
            // .With()
            // .DefaultBuilder()
            // .UseTransport<Msmq>()
            // .UnicastBus()
            // .LoadMessageHandlers()
            // .CreateBus()
            // .Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());

            //while (true)
            //{

            //    Console.ReadLine();
            //  //  var ch = new DemoWebApp.ChatHub();
            //    Bus.Publish(new Chat.Messages.ChatMessage {
            //        Name = "MessageShooter",
            //        Message = "Hello from message shooter"
            //    });
            //}
        }
    }
}

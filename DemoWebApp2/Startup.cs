using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Messaging;
using NServiceBus;
using Owin;

namespace DemoWebApp
{
    public class Startup
    {
        public IBus Bus;

        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();

            Bus = Configure
                .With()
                .DefaultBuilder()
                .UseTransport<Msmq>()
                .UnicastBus()
                .LoadMessageHandlers()
                .CreateBus()
                .Start();

            var config = new ScaleoutConfiguration() { MaxQueueLength = 100 }; // Or whatever you want
            GlobalHost.DependencyResolver.UseNServiceBus(Bus, config);
        }
    }
}
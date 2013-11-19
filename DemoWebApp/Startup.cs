using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Messaging;
using NServiceBus;
using NServiceBus.Installation.Environments;
using Owin;

namespace DemoWebApp
{
    public class Startup
    {
        public static IBus Bus { get; set; }

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
                .Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());

            var config = new ScaleoutConfiguration() { MaxQueueLength = 100 }; // Or whatever you want
            GlobalHost.DependencyResolver.UseNServiceBus(Bus, config);
        }
    }
}
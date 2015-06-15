using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Messaging;
using NServiceBus;
using Owin;
using SignalR.NServiceBus;
using SignalR.NServiceBus.Endpoint;

namespace DemoWebApp
{
    public class Startup
    {
        public IBus Bus;

        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();

            // Note: to get this working, make sure that the user account your app pool runs under has sufficient access to all queues
            // involved in this setup! That means: all signalr.nservicebus.backplaneservice* queues and the System.Web* queues.

            var busConfiguration = new BusConfiguration();
            busConfiguration.UseTransport<MsmqTransport>();
            busConfiguration.UsePersistence<InMemoryPersistence>();


            busConfiguration.RegisterComponents(x => x.ConfigureComponent<NServiceBusMessageBus>(() => GlobalHost.DependencyResolver.Resolve<NServiceBusMessageBus>(), DependencyLifecycle.SingleInstance));
            busConfiguration.EnableInstallers();
            Bus = NServiceBus.Bus.Create(busConfiguration).Start();

            GlobalHost.DependencyResolver.UseNServiceBus(Bus, new ScaleoutConfiguration {MaxQueueLength = 100});// Or whatever you want
        }
    }
}
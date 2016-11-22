using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Messaging;
using NServiceBus;
using Owin;
using SignalR.NServiceBus;

namespace DemoWebApp
{
    public class Startup
    {
        public IEndpointInstance _endpointInstance;

        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();

            // Note: to get this working, make sure that the user account your app pool runs under has sufficient access to all queues
            // involved in this setup! That means: all signalr.nservicebus.backplaneservice* queues and the System.Web* queues.

            var cfg = new EndpointConfiguration("system.web");
            cfg.UseTransport<MsmqTransport>();
            cfg.UsePersistence<InMemoryPersistence>();

            cfg.RegisterComponents(x => x.ConfigureComponent<NServiceBusMessageBus>(() => GlobalHost.DependencyResolver.Resolve<NServiceBusMessageBus>(), DependencyLifecycle.SingleInstance));
            cfg.EnableInstallers();

            _endpointInstance = NServiceBus.Endpoint
                .Create(cfg).ConfigureAwait(false).GetAwaiter().GetResult()
                .Start().ConfigureAwait(false).GetAwaiter().GetResult();

            GlobalHost.DependencyResolver.UseNServiceBus(_endpointInstance, new ScaleoutConfiguration {MaxQueueLength = 100});// Or whatever you want
        }
    }
}
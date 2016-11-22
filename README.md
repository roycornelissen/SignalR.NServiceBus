SignalR.NServiceBus
===================

NServiceBus backplane for SignalR

To use this backplane in a SignalR host, add a reference to SignalR.NServiceBus, initialize an `IEndpointInstance` and configure SignalR to use NServiceBus. In ASP.NET, this would look something like this:

In Startup.cs:

		using Microsoft.AspNet.SignalR;
		using Microsoft.AspNet.SignalR.Messaging;
		using NServiceBus;
		using Owin;

		namespace DemoWebApp
		{
			public class Startup
			{
				public IEndpointInstance _endpointInstance;

				public void Configuration(IAppBuilder app)
				{
					// Any connection or hub wire up and configuration should go here
					app.MapSignalR();

					var cfg = new EndpointConfiguration("system.web");
					cfg.UseTransport<MsmqTransport>();
					cfg.UsePersistence<InMemoryPersistence>();

					cfg.RegisterComponents(x => x.ConfigureComponent<NServiceBusMessageBus>(() => GlobalHost.DependencyResolver.Resolve<NServiceBusMessageBus>(), DependencyLifecycle.SingleInstance));

					_endpointInstance = NServiceBus.Endpoint
						.Create(cfg).ConfigureAwait(false).GetAwaiter().GetResult()
						.Start().ConfigureAwait(false).GetAwaiter().GetResult();

					var scaleoutConfig = new ScaleoutConfiguration {MaxQueueLength = 100};// Or whatever you want
					GlobalHost.DependencyResolver.UseNServiceBus(_endpointInstance, scaleoutConfig);
				}
			}
		}
	
The website in the demo project is using IIS Express. If using IIS, you must make sure that the App Pool user has permission to access the queues involved in this sample: all queues starting with signalr.nservicebus.backplaneservice and all queues starting with system.web:

- signalr.nservicebus.backplaneservice
- signalr.nservicebus.backplaneservice.retries
- signalr.nservicebus.backplaneservice.timeouts
- signalr.nservicebus.backplaneservice.timeoutdispatcher

- system.web
- system.web.retries
- system.web.timeouts
- system.web.timeoutdispatcher

Also, make sure that you have configured the endpoint of the backplane in the config of your SignalR host:

        <configuration>
          <configSections>
            <section name="UnicastBusConfig" type="NServiceBus.Config.UnicastBusConfig, NServiceBus.Core" />
            <section name="MessageForwardingInCaseOfFaultConfig" type="NServiceBus.Config.MessageForwardingInCaseOfFaultConfig, NServiceBus.Core" />
          </configSections>
          <UnicastBusConfig>
            <MessageEndpointMappings>
              <!-- the endpoint on which the backplane is listening for commands -->
              <!-- SignalR will subscribe to new messages via that endpoint -->
              <add Messages="SignalR.NServiceBus" Endpoint="signalr.nservicebus.backplane" />
            </MessageEndpointMappings>
          </UnicastBusConfig>
        
          <MessageForwardingInCaseOfFaultConfig ErrorQueue="error" />
        </configuration>

To run the backplane itself, just start the SignalR.NServiceBus.BackplaneService project (it uses the NServiceBus generic host).

Author
======
[Roy Cornelissen](http://about.me/roycornelissen)

For more info, see my [blogpost](http://roycornelissen.wordpress.com/2013/03/11/an-nservicebus-backplane-for-signalr/)

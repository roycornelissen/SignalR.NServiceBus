SignalR.NServiceBus
===================

NServiceBus backplane for SignalR

To use this backplane in a SignalR host, add a reference to SignalR.NServiceBus, initialize an IBus and configure SignalR to use NServiceBus. In ASP.NET, this would look something like this:

In Global.asax.cs:

        public static IBus Bus { get; set; }

        protected void Application_Start()
        {
            Bus = Configure
                .With()
                    .DefaultBuilder()
                    .MsmqTransport()
                    .UnicastBus()
                        .LoadMessageHandlers()
                        .CreateBus()
                        .Start();

            GlobalHost.DependencyResolver.UseNServiceBus(Bus);

            RouteTable.Routes.MapHubs();
            
            // other initialization
      }

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
              <add Messages="SignalR.NServiceBus" Endpoint="backplanequeue@someserver" />
            </MessageEndpointMappings>
          </UnicastBusConfig>
        
          <MessageForwardingInCaseOfFaultConfig ErrorQueue="error" />
        </configuration>

To run the backplane itself, just start this project (it uses the NServiceBus generic host).

Author
======
[Roy Cornelissen](http://about.me/roycornelissen)

For more info, see my [blogpost](http://roycornelissen.wordpress.com/2013/03/11/an-nservicebus-backplane-for-signalr/)

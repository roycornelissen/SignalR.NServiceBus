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

To run the backplane itself, just start this project (it uses the NServiceBus generic host).

Author
======
[Roy Cornelissen](http://about.me/roycornelissen)

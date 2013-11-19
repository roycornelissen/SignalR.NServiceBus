using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NServiceBus;

namespace DemoWebApp
{
    /// <summary>
    /// Configuration for the webapp's bus.
    /// </summary>
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
    }
}
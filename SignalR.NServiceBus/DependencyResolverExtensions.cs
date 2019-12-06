﻿using System;
using Microsoft.AspNet.SignalR.Messaging;
using SignalR.NServiceBus;
using NServiceBus;

namespace Microsoft.AspNet.SignalR
{
    /// <summary>
    /// Extension method for configuring SignalR.
    /// </summary>
    public static class DependencyResolverExtensions
    {
        /// <summary>
        /// Use NServiceBus backplane for SignalR.
        /// </summary>
        /// <param name="resolver">The dependency resolver.</param>
        /// <param name="busInstance">The instance of the NServiceBus IBus instance inside the current host.</param>
        /// <param name="configuration">Scaleout configuration parameters to be used by SignalR.</param>
        /// <returns>The dependency resolver.</returns>
        public static IDependencyResolver UseNServiceBus(this IDependencyResolver resolver, IEndpointInstance endpointInstance, ScaleoutConfiguration configuration)
        {
            var bus = new Lazy<NServiceBusMessageBus>(() => new NServiceBusMessageBus(resolver, endpointInstance, configuration));
            resolver.Register(typeof(IMessageBus), () => bus.Value);
            resolver.Register(typeof(NServiceBusMessageBus), () => bus.Value);
            return resolver;
        }
    }
}

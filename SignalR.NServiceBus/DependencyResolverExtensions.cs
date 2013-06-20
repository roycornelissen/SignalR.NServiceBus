using System;
using System.Globalization;
using System.Runtime.CompilerServices;
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
        public static IDependencyResolver UseNServiceBus(this IDependencyResolver resolver, IBus busInstance, ScaleoutConfiguration configuration)
        {
            var bus = new Lazy<NServiceBusMessageBus>(() => new NServiceBusMessageBus(resolver, busInstance, configuration));
            resolver.Register(typeof(IMessageBus), () => bus.Value);
            return resolver;
        }
    }
}

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.AspNet.SignalR.Messaging;
using SignalR.NServiceBus;
using NServiceBus;

namespace Microsoft.AspNet.SignalR
{
    public static class DependencyResolverExtensions
    {
        /// <summary>
        /// Use NServiceBus backplane for SignalR.
        /// </summary>
        /// <param name="resolver">The dependency resolver.</param>
        /// <param name="busInstance">The instance of the NServiceBus IBus instance inside the current host.</param>
        /// <returns>The dependency resolver.</returns>
        public static IDependencyResolver UseNServiceBus(this IDependencyResolver resolver, IBus busInstance)
        {
            var bus = new Lazy<NServiceBusMessageBus>(() => new NServiceBusMessageBus(resolver, busInstance));
            resolver.Register(typeof(IMessageBus), () => bus.Value);
            return resolver;
        }
    }
}

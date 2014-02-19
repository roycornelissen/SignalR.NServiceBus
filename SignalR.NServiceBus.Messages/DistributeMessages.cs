using NServiceBus;
using System;

namespace SignalR.NServiceBus.Messages
{
    /// <summary>
    /// Command that instructs the backplane to distribute the payload to all SignalR nodes.
    /// </summary>
    public class DistributeMessages: ICommand
    {
        /// <summary>
        /// The Stream Index Id.
        /// </summary>
        public int StreamIndex { get; set; }

        /// <summary>
        /// The payload consisting of a serialized IList of SignalR messages.
        /// </summary>
        public byte[] Payload { get; set; }
    }
}

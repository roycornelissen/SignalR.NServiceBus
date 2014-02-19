using NServiceBus;
using System;

namespace SignalR.NServiceBus.Messages
{
    /// <summary>
    /// Published when the backplane receives a set of SignalR messages that must be distributed.
    /// </summary>
    public class MessagesAvailable: IEvent
    {
        /// <summary>
        /// Stream Index Id
        /// </summary>
        public int StreamIndex { get; set; }

        /// <summary>
        /// Unique payload Id
        /// </summary>
        public ulong PayloadId { get; set; }

        /// <summary>
        /// Serialized IList of Messages
        /// </summary>
        public byte[] Payload { get; set; }
    }
}

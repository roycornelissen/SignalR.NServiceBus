using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageShooter.Messages.Commands
{
    public class ChatCommand : ICommand
    {
        public string ClientId { get; set; }
    }
}

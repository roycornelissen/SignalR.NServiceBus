using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace MessageShooter
{
    class EndpointConfig: IConfigureThisEndpoint, AsA_Publisher
    {
    }
}

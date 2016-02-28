using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace Jungletribes_Common
{
    public interface NetworkState
    {
        void Synchronise();
        void Synchronise(NetIncomingMessage var);
    }
}

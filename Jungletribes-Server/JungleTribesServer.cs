using Lidgren.Network;
using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace Jungletribes_Server
{
    internal class JungleTribesServer
    {

        public bool Running=true;
        WorldStateServer WSS;
        public JungleTribesServer()
        {
           // new Thread(new ThreadStart(ListenLoop)).Start();

            var config = new NetPeerConfiguration("Jungletribes")
            { Port = 7777 };
            var server = new NetServer(config);
            server.Start();
            WSS = new WorldStateServer(server);
            var update = new FrameController(10, 30, false, Update);

            while (Running)
            {
                update.TryFrame();
            }
            Console.ReadLine();
        }

        public void Update(double delta_time)
        {
            WSS.Update();

        }
    }
}
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

        public JungleTribesServer()
        {
            new Thread(new ThreadStart(ListenLoop)).Start();


            var update = new FrameController(10, 30, false, Update);

            while (Running)
            {
                update.TryFrame();
            }
            Console.ReadLine();
        }

        private void ListenLoop()
        {
            var config = new NetPeerConfiguration("Jungletribes")
            { Port = 7777 };
            var server = new NetServer(config);
            server.Start();
            WorldStateServer WSS = new WorldStateServer(server);
            while (true)
            {
                WSS.Update();
            }
        }

        public void Update(double delta_time)
        {
        }
    }
}
using Lidgren.Network;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace Jungletribes_Server
{
    internal class JungleTribesServer
    {

        public bool Running = true;
        private WorldStateServer WSS;
        private List<Room> rooms;
        private readonly GameTime _gameTime = new GameTime();
        private Stopwatch _gameTimer;
        public JungleTribesServer()
        {
            // new Thread(new ThreadStart(ListenLoop)).Start();
            _gameTimer = new Stopwatch();
            var config = new NetPeerConfiguration("Jungletribes") { Port = 7777 };
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
            _gameTime.TotalGameTime += TimeSpan.FromSeconds(delta_time);
            _gameTime.ElapsedGameTime = TimeSpan.FromSeconds(delta_time);
            WSS.Update();
            foreach (Player p in WSS._Players)
            {
                if (p._PlayerState == EnumPlayerState.SearchingGame)
                {
                    ProcessWaitingPlayer(p);
                }
            }

            foreach (Room r in rooms)
            {
                r.Update(_gameTime);
                r.Draw(_gameTime);
            }

        }

        private void ProcessWaitingPlayer(Player p)
        {
            if (rooms.Count == 0)
            {
                AddPlayerToRoom(p, new Room());
            }
            else
            {
                int i = 0;
                while (rooms[i]._RoomState != EnumRoomState.Avaible && i < rooms.Count)
                    i++;
                if (i > rooms.Count)
                {
                    AddPlayerToRoom(p, new Room());
                }
                else
                {
                    AddPlayerToRoom(p, rooms[i], false);
                }
            }
        }

        private void AddPlayerToRoom(Player p, Room r, bool haveToAddRoomToList = true)
        {
            p._Room = r;
            p._PlayerState = EnumPlayerState.InGame;
            r.AddPlayerToRoom(p);
            if (haveToAddRoomToList)
                rooms.Add(r);
        }
    }
}
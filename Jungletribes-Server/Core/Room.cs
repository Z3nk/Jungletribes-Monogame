using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungletribes_Server
{
    public class Room
    {
        public EnumRoomState _RoomState;
        public readonly int MaxPlayer = 16;
        private List<Player> _Players;
        public Room()
        {
            _RoomState = EnumRoomState.Avaible;
            _Players = new List<Player>();
        }

        public void AddPlayerToRoom(Player p)
        {
            _Players.Add(p);
            if (_Players.Count >= MaxPlayer)
                _RoomState = EnumRoomState.Full;
        }

        public void Update(GameTime time)
        {
            if (_Players != null)
                foreach(Player p in _Players)
                    p.Update(time);
        }

        public void Draw(GameTime time)
        {
            if (_Players != null)
                foreach (Player p in _Players)
                    p.Draw(time);
        }

    }
}

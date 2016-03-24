using Jungletribes_Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Jungletribes_Server
{
    public class Player
    {
        public EnumPlayerState _PlayerState { get; internal set; }
        // To Adjust
        public List<string> keyPressed { get; internal set; }
        public Vector2 _MouseClick { get; internal set; }
        public Vector2 _MousePosition { get; internal set; }
        public IPEndPoint _EndPoint { get; internal set; }
        public Room _Room { get; internal set; }
        public Element _Element { get; internal set; }
        public string _NamePlayer { get; internal set; }

        public void Update(GameTime time)
        {
            if(_Element!=null)
            _Element.Update(time);
        }

        public void Draw(GameTime time)
        {
            if (_Element != null)
                _Element.Draw(time);
        }
    }
}

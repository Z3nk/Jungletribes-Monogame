using Jungletribes_Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungletribes_Server
{
    public class Player
    {
        public EnumPlayerState _PlayerState { get; set; }
        public Vector2 _MouseClick { get; set; }
        public Vector2 _MousePosition { get; internal set; }
        public Element element;
        public string namePlayer;

        public void Update(GameTime time)
        {
            element.Update(time);
        }

        public void Draw(GameTime time)
        {
            element.Draw(time);
        }
    }
}

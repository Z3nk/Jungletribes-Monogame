using Jungletribes_Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungletribes
{
    public abstract class Pipe
    {
        public static Vector2 MouseClick { get; set; }
        public static Vector2 MousePosition { get; internal set; }
        public static Element player;
    }
}

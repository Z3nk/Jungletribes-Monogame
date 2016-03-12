using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungletribes
{
    public abstract class Helper
    {
        public static Vector2 getVectorToPoint(Vector2 position, Vector2 destination, Vector2 force)
        {
            var x = (destination.X - position.X) * force.X;
            var y = (destination.Y - position.Y) * force.Y;
            return Vector2.Normalize(new Vector2(x, y));

            //var x = (Pipe.MouseClickX - position.X) * this.speed.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //var y = (Pipe.MouseClickY - position.Y) * this.speed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //MoveToDo = Vector2.Normalize(new Vector2(x, y));
        }
    }
}

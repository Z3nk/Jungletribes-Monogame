using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungletribes
{
    public class Circle
    {
        public Vector2 pos
        {
            get; set;
        }

        int r;

        public Circle(Vector2 pos, int r)
        {
            this.pos = pos;
            this.r = r;
        }
        public Rectangle getAABB()
        {
            var corner = this.pos-new Vector2(r, r);
            return new Rectangle((int)corner.X,(int)corner.Y, r * 2, r * 2);
        }

        public static bool CircleToCircle(Circle a, Circle b)
        {
            if (!a.getAABB().Intersects(b.getAABB()))
            {
                return false;
            }
            else
            {
                var distance = Math.Sqrt(((a.pos.X - b.pos.X) * (a.pos.X - b.pos.X)) + ((a.pos.Y - b.pos.Y) * (a.pos.Y - b.pos.Y)));
                if (distance < a.r + b.r)
                {
                    return true;
                }
                return false;
            }
        }
    }
}

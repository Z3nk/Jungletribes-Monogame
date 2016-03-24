using Jungletribes_Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace Jungletribes_Server
{ 
    public class CircleElement : ElementPlayable
    {
        private static Rectangle rect;

        public override Vector2 center
        {
            get
            {
                return new Vector2(position.X + rect.Width / 2, position.Y + rect.Height / 2);
            }
        }
        public Circle collision_circle;

        public CircleElement()
        {
            commands = EnumMoveCommand.None;
            position = new Vector2(0, 0);
            speed = new Vector2(100, 100);

            collision_circle = new Circle(center, rect.Width / 2);
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 MoveToDo = new Vector2(0, 0);
            if (commands.HasFlag(EnumMoveCommand.Left))
            {
                MoveToDo.X -= this.speed.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (commands.HasFlag(EnumMoveCommand.Right))
            {
                MoveToDo.X += this.speed.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (commands.HasFlag(EnumMoveCommand.Up))
            {
                MoveToDo.Y -= this.speed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (commands.HasFlag(EnumMoveCommand.Bottom))
            {
                MoveToDo.Y += this.speed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (commands.HasFlag(EnumMoveCommand.Horizontal) && commands.HasFlag(EnumMoveCommand.Vertical))
            {
                MoveToDo.X = (float)(Math.Cos(45) * MoveToDo.X);
                MoveToDo.Y = (float)(Math.Sin(45) * MoveToDo.Y);
            }
            if (commands.HasFlag(EnumMoveCommand.RightClick))
            {
                MoveToDo = Helper.getVectorToPoint(center, _MyPlayer._MouseClick, this.speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            this.position += MoveToDo;

            collision_circle.pos = center;
        }

        public override void Draw(GameTime gameTime)
        {
          //
        }

        public override void LoadContent()
        {
            isInit = true;
        }

        public override void UnloadContent()
        {
            isInit = false;
        }

        public override void Synchronise()
        {
            throw new NotImplementedException();
        }

        public override void Synchronise(NetIncomingMessage var)
        {
            throw new NotImplementedException();
        }
    }

}

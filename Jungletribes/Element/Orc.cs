using Jungletribes_Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace Jungletribes
{
    public class Orc : Element
    {
        public Vector2 position;
        public Vector2 speed;
        public Animation A_right { get; set; }
        public Animation A_bot { get; set; }
        public Animation A_left { get; set; }
        public Animation A_top { get; set; }

        private Animation _current_animation;
        public Animation current_animation
        {
            get
            {
                if (commands == EnumMoveCommand.None)
                    current_animation = A_bot;
                if ((commands & EnumMoveCommand.Right) != EnumMoveCommand.None)
                    current_animation = A_right;
                if ((commands & EnumMoveCommand.Left) != EnumMoveCommand.None)
                    current_animation = A_left;
                if ((commands & EnumMoveCommand.Up) != EnumMoveCommand.None)
                    current_animation = A_top;
                if ((commands & EnumMoveCommand.Bottom) != EnumMoveCommand.None)
                    current_animation = A_bot;

                return _current_animation;
            }
            set
            {
                _current_animation = value;
            }
        }

        private static Texture2D Orc_Texture { get; set; }
        public Orc()
        {
            commands = EnumMoveCommand.None;
            A_right = new Animation();
            for (int i = 0; i < 9; i++)
                A_right.AddFrame(new Rectangle(i * 64, 11 * 64, 64, 64), TimeSpan.FromSeconds(.1));
            A_bot = new Animation();
            for (int i = 0; i < 9; i++)
                A_bot.AddFrame(new Rectangle(i * 64, 10 * 64, 64, 64), TimeSpan.FromSeconds(.1));
            A_left = new Animation();
            for (int i = 0; i < 9; i++)
                A_left.AddFrame(new Rectangle(i * 64, 9 * 64, 64, 64), TimeSpan.FromSeconds(.1));
            A_top = new Animation();
            for (int i = 0; i < 9; i++)
                A_top.AddFrame(new Rectangle(i * 64, 8 * 64, 64, 64), TimeSpan.FromSeconds(.1));
            if (Orc_Texture == null)
                using (var stream = TitleContainer.OpenStream("Content/orc.png"))
                {
                    Orc_Texture = Texture2D.FromStream(JungleTribesGame.Instance.GraphicsDevice, stream);
                }
            position = new Vector2(0, 0);
            speed = new Vector2(100, 100);
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 MoveToDo = new Vector2(0, 0);
            if ((commands & EnumMoveCommand.Left) != EnumMoveCommand.None)
            {
                MoveToDo.X -= this.speed.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if ((commands & EnumMoveCommand.Right) != EnumMoveCommand.None)
            {
                MoveToDo.X += this.speed.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if ((commands & EnumMoveCommand.Up) != EnumMoveCommand.None)
            {
                MoveToDo.Y -= this.speed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if ((commands & EnumMoveCommand.Bottom) != EnumMoveCommand.None)
            {
                MoveToDo.Y += this.speed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if ((commands & EnumMoveCommand.Horizontal) != EnumMoveCommand.None && (commands & EnumMoveCommand.Vertical) != EnumMoveCommand.None)
            {
                MoveToDo.X = (float)(Math.Cos(45) * MoveToDo.X);
                MoveToDo.Y = (float)(Math.Sin(45) * MoveToDo.Y);
            }

            this.position += MoveToDo;

            current_animation.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            var sourceRectangle = current_animation.CurrentRectangle;
            JungleTribesGame.Instance.spriteBatch.Draw(Orc_Texture, new Rectangle((int)this.position.X, (int)this.position.Y, sourceRectangle.Width, sourceRectangle.Height), sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 1.0f);
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

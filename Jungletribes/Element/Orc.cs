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
        public Vector2 position { get; set; }
        public Animation A_right { get; set; }
        public Animation A_bot { get; set; }
        public Animation A_left { get; set; }
        public Animation A_top { get; set; }

        public EnumMoveCommand commands;
        private Animation _current_animation;
        public Animation current_animation
        {
            get
            {
                switch (commands)
                {
                    case EnumMoveCommand.None:
                        current_animation = A_bot;
                        break;
                    case EnumMoveCommand.Right:
                        current_animation = A_right;
                        break;
                    case EnumMoveCommand.Left:
                        current_animation = A_left;
                        break;
                    case EnumMoveCommand.Up:
                        current_animation = A_top;
                        break;
                    case EnumMoveCommand.Bottom:
                        current_animation = A_bot;
                        break;
                    default:
                        current_animation = A_bot;
                        break;
                }
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
        }

        public override void Update(GameTime gameTime)
        {
            if (JungleTribesGame.Instance.KeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q))
                 commands = EnumMoveCommand.Left;
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

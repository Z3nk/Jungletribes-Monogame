using Jungletribes_Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungletribes.Element
{
    public class Orc
    {
        public Vector2 position { get; set; }
        public Animation A_right { get; set; }
        public Animation A_bot { get; set; }
        public Animation A_left { get; set; }
        public Animation A_top { get; set; }
        public Animation current_animation { get; set; }

        private static Texture2D Orc_Texture { get; set; }
        public Orc()
        {
            A_right = new Animation();
            for (int i = 0; i < 9; i++)
                A_right.AddFrame(new Rectangle(i * 64, 11 * 64, 64, 64), TimeSpan.FromSeconds(.1));
            A_bot = new Animation();
            for (int i = 0; i < 9; i++)
                A_bot.AddFrame(new Rectangle(i * 64, 10 * 64, 64, 64), TimeSpan.FromSeconds(.1));
            A_left = new Animation();
            for (int i = 0; i < 9; i++)
                A_bot.AddFrame(new Rectangle(i * 64, 9 * 64, 64, 64), TimeSpan.FromSeconds(.1));
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

        public void Update(GameTime gameTime)
        {
            A_right.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            var sourceRectangle = A_right.CurrentRectangle;
            JungleTribesGame.Instance.spriteBatch.Draw(Orc_Texture, new Rectangle((int)this.position.X, (int)this.position.Y, sourceRectangle.Width, sourceRectangle.Height), sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 1.0f);
        }
    }
}

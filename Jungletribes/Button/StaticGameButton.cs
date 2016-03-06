using Jungletribes_Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungletribes
{
    public class StaticGameButton : Button
    {
        public Vector2 size
        {
            get;
            set;
        }
        private static Texture2D texture
        {
            get;
            set;
        }
        private readonly String buttonPNG = "Content/GUI_static_game_button_flat.png";

        public StaticGameButton()
        {
            if (texture == null)
            {
                using (var stream = TitleContainer.OpenStream(buttonPNG))
                {
                    texture = Texture2D.FromStream(JungleTribesGame.Instance.GraphicsDevice, stream);
                }
            }

            this.position = new Vector2(0, 0);
            this.size = new Vector2(512, 192);
        }

        public void Update(GameTime gameTime)
        {
            //TouchCollection touchCollection = TouchPanel.GetState();
            //if (touchCollection.Count > 0)
            //{
            //    Rectangle bound = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            //    int mouseX = (int)Resolution.AdjustWidthWithScren(touchCollection[0].Position.X);
            //    int mouseY = (int)Resolution.AdjustWidthWithScren(touchCollection[0].Position.Y);
            //    if (bound.Contains(mouseX, mouseY))
            //    {
            //        onClick();
            //    }
            //}
            var mouseState = Mouse.GetState();
            Rectangle bound = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            int mouseX = (int)Resolution.AdjustWidthWithScren(mouseState.Position.X);
            int mouseY = (int)Resolution.AdjustWidthWithScren(mouseState.Position.Y);
            if (bound.Contains(mouseX, mouseY))
            {
                onClick();
            }


        }

        private void onClick()
        {
            ScreenManager.moveTo(GameScreen.name);
        }

        public void Draw()
        {
            JungleTribesGame.Instance.spriteBatch.Draw(texture, new Rectangle((int)this.position.X, (int)this.position.Y, texture.Width, texture.Height), new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.6f);
        }
    }
}

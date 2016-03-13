using Jungletribes_Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungletribes
{
    public class MenuScreen : Screen
    {
        private static Texture2D wallScreenBot;
        private static Texture2D wallScreenUp;
        private static Texture2D background;
        public static readonly String name = "menuScreen";
        public static TimeSpan timer = TimeSpan.FromSeconds(0);
        public KeyboardState KeyboardState;
        private StaticGameButton button;

        public MenuScreen()
        {
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
            isInit = true;
            int width = Int32.Parse(ConfigurationManager.AppSettings["GUI_static_game_button_flat_width"]);
            int height = Int32.Parse(ConfigurationManager.AppSettings["GUI_static_game_button_flat_height"]);
            string path = ConfigurationManager.AppSettings["GUI_static_game_button_flat_path"];
            button = new StaticGameButton(JungleTribesGame.Instance.widthScreen / 2 - width / 2, JungleTribesGame.Instance.heightScreen / 2 - height / 2, path);
            button.onClick += Button_onClick;

            using (var stream = TitleContainer.OpenStream("Content/support_Button.jpg"))
            {
                wallScreenBot = Texture2D.FromStream(JungleTribesGame.Instance.GraphicsDevice, stream);
            }
            using (var stream = TitleContainer.OpenStream("Content/support_Button_haut.jpg"))
            {
                wallScreenUp = Texture2D.FromStream(JungleTribesGame.Instance.GraphicsDevice, stream);
            }
            using (var stream = TitleContainer.OpenStream("Content/background.jpg"))
            {
                background = Texture2D.FromStream(JungleTribesGame.Instance.GraphicsDevice, stream);
            }

        }

        private void Button_onClick()
        {
            //ScreenManager.moveTo(GameScreen.name);
            ScreenManager.moveTo(GameScreen.name);
        }

        public override void UnloadContent()
        {
            isInit = false;
            JungleTribesGame.Instance.Content.Unload();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public override void Update(GameTime gameTime)
        {
            button.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //JungleTribesGame.Instance.spriteBatch.Draw(background, new Rectangle(0, 0, background.Width, background.Height), Color.White);
            for (int i = 0; i < 10; i++)
            {
                JungleTribesGame.Instance.spriteBatch.Draw(wallScreenUp, new Rectangle(wallScreenUp.Width*i, 0, wallScreenUp.Width, wallScreenUp.Height), Color.White);
                JungleTribesGame.Instance.spriteBatch.Draw(wallScreenBot, new Rectangle(wallScreenUp.Width * i, JungleTribesGame.Instance.heightScreen - wallScreenBot.Height, wallScreenBot.Width, wallScreenBot.Height), Color.White);
            }
            button.Draw();
        }
    }
}

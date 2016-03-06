using Jungletribes_Common;
using Microsoft.Xna.Framework;
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
        }

        private void Button_onClick()
        {
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
            button.Draw();
        }
    }
}

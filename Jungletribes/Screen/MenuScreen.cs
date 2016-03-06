using Jungletribes_Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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
            button = new StaticGameButton();
            float X = JungleTribesGame.Instance.widthScreen / 2 - button.size.X / 2;
            float Y = JungleTribesGame.Instance.heightScreen / 2 - (button.size.Y / 2 + 5);
            button.position = new Vector2(X, Y);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jungletribes_Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Jungletribes
{
    public class GameScreen : Screen
    {
        public static readonly String name = "gameScreen";
        public static TimeSpan timer = TimeSpan.FromSeconds(0);
        private Orc test;
        private Element player;
        public KeyboardState KeyboardState;

        public GameScreen()
        {
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
            isInit = true;
            test = new Orc();
            player = test;
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
            KeyboardState = Keyboard.GetState();
            player.commands = EnumMoveCommand.None;
            if (KeyboardState.IsKeyDown(Keys.Q))
            {
                player.commands = player.commands | EnumMoveCommand.Left;
            }
            if (KeyboardState.IsKeyDown(Keys.D))
            {
                player.commands = player.commands | EnumMoveCommand.Right;
            }
            if (KeyboardState.IsKeyDown(Keys.Z))
            {
                player.commands = player.commands | EnumMoveCommand.Up;
            }
            if (KeyboardState.IsKeyDown(Keys.S))
            {
                player.commands = player.commands | EnumMoveCommand.Bottom;
            }
            test.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            test.Draw(gameTime);
        }



    }
}

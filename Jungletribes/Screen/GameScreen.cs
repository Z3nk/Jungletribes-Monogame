using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jungletribes_Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Jungletribes
{
    public class GameScreen : Screen
    {
        public static readonly String name = "gameScreen";
        public static TimeSpan timer = TimeSpan.FromSeconds(0);
        private CircleElement test;
        private Texture2D floor;
        public KeyboardState KeyboardState;
        public MouseState _mouseState;

        public GameScreen()
        {
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
            isInit = true;
            using (var stream = TitleContainer.OpenStream("Content/leaves.png"))
            {
                floor = Texture2D.FromStream(JungleTribesGame.Instance.GraphicsDevice, stream);
            }
            test = new CircleElement();
            Pipe.player = test;
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
            #region Keyboard
            KeyboardState = Keyboard.GetState();
            Pipe.player.commands = EnumMoveCommand.None;
            if (KeyboardState.IsKeyDown(Keys.Q))
            {
                Pipe.player.commands = Pipe.player.commands | EnumMoveCommand.Left;
            }
            if (KeyboardState.IsKeyDown(Keys.D))
            {
                Pipe.player.commands = Pipe.player.commands | EnumMoveCommand.Right;
            }
            if (KeyboardState.IsKeyDown(Keys.Z))
            {
                Pipe.player.commands = Pipe.player.commands | EnumMoveCommand.Up;
            }
            if (KeyboardState.IsKeyDown(Keys.S))
            {
                Pipe.player.commands = Pipe.player.commands | EnumMoveCommand.Bottom;
            }
            #endregion

            #region Mouse
            _mouseState = Mouse.GetState();
            Pipe.MousePosition = new Vector2(Resolution.AdjustWidthWithScren(_mouseState.Position.X), Resolution.AdjustWidthWithScren(_mouseState.Position.Y));
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                Pipe.MouseClick = new Vector2(Resolution.AdjustWidthWithScren(_mouseState.Position.X), Resolution.AdjustWidthWithScren(_mouseState.Position.Y));
                Pipe.player.commands = Pipe.player.commands | EnumMoveCommand.LeftClick;
            }
            if (_mouseState.RightButton == ButtonState.Pressed)
            {
                Pipe.MouseClick = new Vector2(Resolution.AdjustWidthWithScren(_mouseState.Position.X), Resolution.AdjustWidthWithScren(_mouseState.Position.Y));
                Pipe.player.commands = Pipe.player.commands | EnumMoveCommand.RightClick;
            }
            #endregion

            #region update
                test.Update(gameTime);
            #endregion
        }

        public override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < 15; i++)
                for (int y = 0; y < 15; y++)
                    JungleTribesGame.Instance.spriteBatch.Draw(floor, new Rectangle(i * 128, y * 128, 128, 128), Color.White);
            test.Draw(gameTime);
        }



    }
}

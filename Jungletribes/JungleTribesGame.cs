﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Jungletribes_Common;

namespace Jungletribes
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class JungleTribesGame : Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public static JungleTribesGame Instance;
        public KeyboardState KeyboardState;
        private Orc test;
        private Element player;

        public static readonly int widthScreen = 1920;
        public static readonly int heightScreen = 1080;

        public JungleTribesGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            Resolution.Init(ref graphics);
            Content.RootDirectory = "Content";
            Resolution.SetVirtualResolution(widthScreen, heightScreen);
            Resolution.SetResolution(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, false);
            if (Instance == null)
                Instance = this;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            test = new Orc();
            player = test;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState=Keyboard.GetState();
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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);            
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, Resolution.getTransformationMatrix());
            test.Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

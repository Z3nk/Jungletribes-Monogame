using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Jungletribes_Common;
using System.Configuration;
using System;
using Lidgren.Network;

namespace Jungletribes
{
    public class JungleTribesGame : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public static JungleTribesGame Instance;

        public readonly int widthScreen = 1920;
        public readonly int heightScreen = 1080;
        public static readonly string serverIp = "serverIp";
        public static readonly string serverPort = "serverPort";

        public JungleTribesGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Resolution.Init(ref graphics);
            Content.RootDirectory = "Content";
            Resolution.SetVirtualResolution(widthScreen, heightScreen);
            Resolution.SetResolution(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, false);
            this.IsMouseVisible = true;
            if (Instance == null)
                Instance = this;
        }

        protected override void Initialize()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string IP = appSettings[serverIp] ?? "Not Found";
            int PORT = Int32.Parse(appSettings[serverPort]);
            var config = new NetPeerConfiguration("Jungletribes");
            var client = new NetClient(config);
            client.Start();
            client.Connect(IP, PORT);
            new WorldState(client);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ScreenManager.addScreen(GameScreen.name, new GameScreen());
            ScreenManager.addScreen(MenuScreen.name, new MenuScreen());
            ScreenManager.currentScreen = ScreenManager.getScreen(MenuScreen.name);
            ScreenManager.currentScreen.Initialize();
            ScreenManager.currentScreen.LoadContent();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            ScreenManager.currentScreen.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, Resolution.getTransformationMatrix());
            ScreenManager.currentScreen.Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

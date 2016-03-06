using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Jungletribes_Common;

namespace Jungletribes
{
    public class JungleTribesGame : Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public static JungleTribesGame Instance;

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

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ScreenManager.addScreen(GameScreen.name, new GameScreen(this));
            ScreenManager.currentScreen = ScreenManager.getScreen(GameScreen.name);
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
            GraphicsDevice.Clear(Color.DimGray);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, Resolution.getTransformationMatrix());
            ScreenManager.currentScreen.Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

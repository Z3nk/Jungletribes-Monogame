using Jungletribes_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jungletribes
{

    public class LoadingScreen : Screen
    {
        public static readonly String name = "loadingScreen";
        SpriteFont font;
        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            isInit = true;
            font = JungleTribesGame.Instance.Content.Load<SpriteFont>("Font");
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
            
        }

        public override void Draw(GameTime gameTime)
        {
            JungleTribesGame.Instance.spriteBatch.DrawString(font, "Loading...", new Vector2(JungleTribesGame.Instance.widthScreen / 2, JungleTribesGame.Instance.heightScreen / 2), Color.White);
        }



      
    }
}

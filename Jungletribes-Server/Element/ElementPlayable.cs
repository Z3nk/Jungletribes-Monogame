using Jungletribes_Common;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace Jungletribes_Server
{
    public abstract class ElementPlayable : Element
    {
        public override abstract Vector2 center { get; }

        #region methods
        public override abstract void LoadContent();
        public override abstract void UnloadContent();
        public override abstract void Update(GameTime gameTime);
        public override abstract void Draw(GameTime gameTime);

        public override abstract void Synchronise();
        public override abstract void Synchronise(NetIncomingMessage var);
        #endregion
        public Player _MyPlayer { get; set; }
    }
}

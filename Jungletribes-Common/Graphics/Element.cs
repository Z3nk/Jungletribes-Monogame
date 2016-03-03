using System;
using Microsoft.Xna.Framework;
using Lidgren.Network;
namespace Jungletribes_Common
{
    public abstract class Element : NetworkState
    {
        #region attributes
        private int _id;
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }


        private EnumMoveCommand _commands;
        public EnumMoveCommand commands
        {
            get { return _commands; }
            set { _commands = value; }
        }
        #endregion

        #region methods
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);

        public abstract void Synchronise();
        public abstract void Synchronise(NetIncomingMessage var);
        #endregion
    }
}

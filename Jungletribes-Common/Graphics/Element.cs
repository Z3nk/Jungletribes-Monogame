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

        private bool _isInit=false;
        public bool isInit
        {
            get { return _isInit; }
            set { _isInit = value; }
        }
        #endregion

        #region methods
        public abstract void LoadContent();
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);

        public abstract void Synchronise();
        public abstract void Synchronise(NetIncomingMessage var);
        #endregion
    }
}

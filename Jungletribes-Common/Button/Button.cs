using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Jungletribes_Common
{
    public class Button
    {
        #region Attributes
        public Vector2 position
        {
            get;
            set;
        }
        #endregion

        #region Events
        public delegate void ButtonAction();
        public event ButtonAction onClick;
        protected virtual void onClickEvent()
        {
            onClick();
        }
        #endregion
    }
}
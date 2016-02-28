using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Jungletribes_Common
{
    public class GameAction
    {
        public int angle;
        public Vector2 speed;
        public TimeSpan timer;
        public bool isRotate;
        public bool isPosition;
        public Vector2 position;
        public GameAction(int angle, Vector2 speed, TimeSpan timer, bool isRotate, bool isPosition, Vector2 position)
        {
            this.angle = angle;
            this.speed = speed;
            this.timer = timer;
            this.isRotate = isRotate;
            this.isPosition = isPosition;
            this.position = position;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungletribes_Common
{
    [Flags]
    public enum EnumMoveCommand
    {
        None = 0,
        Up = 1 << 0, //1
        Right = 1 << 1, //2
        Bottom = 1 << 2, //4
        Left = 1 << 3, //8
        LeftClick = 1 << 4, //16
        RightClick = 1 << 5, //32
        Vertical = Up | Bottom,
        Horizontal = Left | Right,
        Move = Up | Right | Left | Bottom
    }
}

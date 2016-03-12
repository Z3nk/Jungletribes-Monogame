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
        None = 0x0,
        Up = 0x1,
        Right = 0x2,
        Bottom = 0x4,
        Left = 0x8,
        LeftClick = 0x16,
        RightClick = 0x32,
        Vertical = Up | Bottom,
        Horizontal = Left | Right,
        Move = Up | Right | Left | Bottom
    }
}

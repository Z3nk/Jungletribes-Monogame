using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungletribes_Common
{
    [Flags]
    public enum EnumActionCommand
    {
        None = 0x0,
        UseItem = 0x1,
        LeftClick = 0x2,
        RightClick = 0x4,
        skill1 = 0x8,
        skill2 = 0x16,
        skill3 = 0x32,
        skill4 = 0x64,
    }
}

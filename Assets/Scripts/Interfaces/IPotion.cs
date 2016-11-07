using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Character;

namespace Items
{
    interface IPotion
    {
        int Statistics { get; set; }
        float BeginValue { get; set; }
        float OverTimeValue { get; set; }
        float Duration { get; set; }
    }
}

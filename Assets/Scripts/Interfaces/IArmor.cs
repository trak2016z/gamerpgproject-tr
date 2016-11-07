using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bonuses;

namespace Items
{
    interface IArmor
    {
        ArmorTypes ArmorType { get; set; }
        List<Bonus> GetBonuses();
        bool AddBonus(Bonus bonus);
    }
}

using System;
using System.Collections.Generic;

namespace Items
{
    interface IWeapon
    {
        WeaponTypes WeaponType { get; set; }
        int MinimalDamage { get; set; }
        int MaximalDamage { get; set; }
        float Range { get; set; }
    }
}
